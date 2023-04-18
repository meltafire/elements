using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class LevelSesstionController
{
    private readonly IMenuProvider _menuProvider;
    private readonly ISwipeHandler _swipeHandler;
    private readonly ISwapHandler _swapHandler;
    private readonly IMovementHandler _movementHandler;
    private readonly IDropHandler _dropHandler;
    private readonly IDestroyHandler _destroyHandler;

    private UniTaskCompletionSource _nextButtonClickCompletionSource;

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
                await Normalize(interactionPositionList, token);

                if (token.IsCancellationRequested)
                {
                    break;
                }

            }

        }

        UnsubscribeFromMenu();
        DeactivateMenu();

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
        var rowList = interactionPositionList.Select(position => position.I).Distinct();
        var columnList = interactionPositionList.Select(position => position.J).Distinct();

        interactionPositionList.Clear();

        await DropBlocks(columnList, token);

        if (token.IsCancellationRequested)
        {
            return false;
        }

        var positionOfDestroyedItems = await DestroyBlocks(rowList, columnList, token);

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

    private async UniTask<IEnumerable<PositionData>> DestroyBlocks(IEnumerable<int> rowList, IEnumerable<int> columnList, CancellationToken token)
    {
        return await _destroyHandler.TryDestroyItems(rowList, columnList, token);
    }

    private void SpawnPlayfield()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}