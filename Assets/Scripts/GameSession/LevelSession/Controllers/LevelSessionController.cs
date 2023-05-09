using Cysharp.Threading.Tasks;
using Elements.GameSession.Data;
using Elements.GameSession.Handlers.Infrastructure;
using Elements.GameSession.Positions.Data;
using Elements.Menu.Providers.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elements.GameSession.Controllers
{
    public class LevelSessionController
    {
        private readonly IMenuProvider _menuProvider;
        private readonly ISwipeHandler _swipeHandler;
        private readonly ISwapHandler _swapHandler;
        private readonly IMovementHandler _movementHandler;
        private readonly IDropHandler _dropHandler;
        private readonly IDestroyHandler _destroyHandler;
        private readonly IGameEndRulesHandler _gameEndRulesHandler;
        private readonly IPlayfieldSpawnerHelper _playfieldSpawnerHelper;

        private UniTaskCompletionSource _nextButtonClickCompletionSource;

        public LevelSessionController(
            IMenuProvider menuProvider,
            ISwipeHandler swipeHandler,
            ISwapHandler swapHandler,
            IMovementHandler movementHandler,
            IDropHandler dropHandler,
            IDestroyHandler destroyHandler,
            IGameEndRulesHandler gameEndRulesHandler,
            IPlayfieldSpawnerHelper playfieldSpawnerHelper)
        {
            _menuProvider = menuProvider;
            _swipeHandler = swipeHandler;
            _swapHandler = swapHandler;
            _movementHandler = movementHandler;
            _dropHandler = dropHandler;
            _destroyHandler = destroyHandler;
            _gameEndRulesHandler = gameEndRulesHandler;
            _playfieldSpawnerHelper = playfieldSpawnerHelper;
        }

        public async UniTask Execute(CancellationToken token)
        {
            var interactionPositionList = new List<PositionData>();

            _nextButtonClickCompletionSource = new UniTaskCompletionSource();
            var tokenRegistration = token.Register(CancelAwaingOfNextButtonClick);

            var isGameEnded = false;

            SpawnPlayfield();
            SubscribeToMenu();

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

            UnsubscribeFromMenu();

            if (!token.IsCancellationRequested)
            {
                DeactivateMenu();
            }

            tokenRegistration.Dispose();

            if (!token.IsCancellationRequested)
            {
                DespawnPlayfield();
            }
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

        private void SpawnPlayfield()
        {
            _playfieldSpawnerHelper.Spawn();
        }

        private void CancelAwaingOfNextButtonClick()
        {
            _nextButtonClickCompletionSource.TrySetCanceled();
        }

        private void OnNextButtonClicked()
        {
            _nextButtonClickCompletionSource.TrySetResult();
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

        private void DespawnPlayfield()
        {
            _playfieldSpawnerHelper.Despawn();
        }
    }
}