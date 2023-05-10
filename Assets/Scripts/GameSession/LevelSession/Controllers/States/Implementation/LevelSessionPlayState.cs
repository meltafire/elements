using Cysharp.Threading.Tasks;
using Elements.GameSession.Handlers.Infrastructure;
using Elements.GameSession.LevelSession.Controllers.States.Infrastructure;
using Elements.GameSession.Positions.Data;
using Elements.Menu.Providers.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elements.GameSession.LevelSession.Controllers.States.Implementation
{
    public class LevelSessionPlayState : ILevelSessionState
    {
        private readonly IMenuProvider _menuProvider;
        private readonly IGameEndRulesHandler _gameEndRulesHandler;
        private readonly ISwipeHandler _swipeHandler;
        private readonly ISwapHandler _swapHandler;
        private readonly IMovementHandler _movementHandler;
        private readonly IDropHandler _dropHandler;
        private readonly IDestroyHandler _destroyHandler;
        private readonly LevelSessionDespawnState _levelSessionDespawnState;

        private UniTaskCompletionSource _nextButtonClickCompletionSource;

        public LevelSessionPlayState(
            IMenuProvider menuProvider,
            IGameEndRulesHandler gameEndRulesHandler
            , ISwipeHandler swipeHandler,
            ISwapHandler swapHandler,
            IMovementHandler movementHandler,
            IDropHandler dropHandler,
            IDestroyHandler destroyHandler,
            LevelSessionDespawnState levelSessionDespawnState)
        {
            _menuProvider = menuProvider;
            _gameEndRulesHandler = gameEndRulesHandler;
            _swipeHandler = swipeHandler;
            _swapHandler = swapHandler;
            _movementHandler = movementHandler;
            _dropHandler = dropHandler;
            _destroyHandler = destroyHandler;
            _levelSessionDespawnState = levelSessionDespawnState;
        }

        public async UniTask Execute(LevelSessionController contextController, CancellationToken token)
        {
            _nextButtonClickCompletionSource = new UniTaskCompletionSource();
            var tokenRegistration = token.Register(CancelAwaingOfNextButtonClick);

            SubscribeToMenu();

            var isGameEnded = false;
            var interactionPositionList = new List<PositionData>();

            while (!isGameEnded)
            {
                ActivateMenu();

                await UniTask.WhenAny(HandlePlayerMove(interactionPositionList, token), _nextButtonClickCompletionSource.Task);

                if (token.IsCancellationRequested || _nextButtonClickCompletionSource.Task.Status == UniTaskStatus.Succeeded)
                {
                    break;
                }

                DeactivateMenu();

                var isFieldNormalized = false;
                while (!isFieldNormalized)
                {
                    isFieldNormalized = await Normalize(interactionPositionList, token);

                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                }

                isGameEnded = _gameEndRulesHandler.CheckGameCompletion();
            }

            tokenRegistration.Dispose();

            UnsubscribeFromMenu();

            if (!token.IsCancellationRequested)
            {
                DeactivateMenu();
            }

            contextController.ChangeState(_levelSessionDespawnState);
        }

        private void SubscribeToMenu()
        {
            _menuProvider.OnNextButtonClick += OnNextButtonClicked;
        }

        private void UnsubscribeFromMenu()
        {
            _menuProvider.OnNextButtonClick -= OnNextButtonClicked;
        }

        private void ActivateMenu()
        {
            _menuProvider.SetButtonActive(true);
        }

        private void DeactivateMenu()
        {
            _menuProvider.SetButtonActive(false);
        }

        private void OnNextButtonClicked()
        {
            _nextButtonClickCompletionSource.TrySetResult();
        }

        private void CancelAwaingOfNextButtonClick()
        {
            _nextButtonClickCompletionSource.TrySetCanceled();
        }

                private async UniTask HandlePlayerMove(List<PositionData> interactionPositionList, CancellationToken token)
        {
            var result = await _swipeHandler.Handle(token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            _swapHandler.Execute(result);

            interactionPositionList.Add(result.FromPosition);
            interactionPositionList.Add(result.ToPosition);

            await _movementHandler.Execute(interactionPositionList, token);
        }

        private async UniTask<bool> Normalize(List<PositionData> interactionPositionList, CancellationToken token)
        {
            var columnList = interactionPositionList.GroupBy(data => data.I).Select(g => g.First()).Select(data => data.I).ToList();

            interactionPositionList.Clear();

            await DropBlocks(columnList, token);

            if (token.IsCancellationRequested)
            {
                return false;
            }

            var positionOfDestroyedItems = await DestroyBlocks(token);

            var isNothingDestroyed = positionOfDestroyedItems.Count() == 0;
            if (!isNothingDestroyed)
            {
                foreach (var item in positionOfDestroyedItems)
                {
                    interactionPositionList.Add(item);
                }

            }

            return isNothingDestroyed;
        }

        private UniTask DropBlocks(IEnumerable<int> columnList, CancellationToken token)
        {
            var positionList = _dropHandler.CalculateDropPositions(columnList);

            return _movementHandler.Execute(positionList, token);
        }

        private async UniTask<IEnumerable<PositionData>> DestroyBlocks(CancellationToken token)
        {
            return await _destroyHandler.TryDestroyItems(token);
        }
    }
}
