using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class LevelSesstionController
{
    private readonly IMenuProvider _menuProvider;
    private readonly ISwipeHandler _swipeHandler;
    private readonly ISwapHandler _swapHandler;
    private readonly IMovementHandler _movementHandler;
    private readonly List<PositionData> _positionList;

    private UniTaskCompletionSource _nextButtonClickCompletionSource;

    public async UniTask Execute(CancellationToken token)
    {
        _nextButtonClickCompletionSource = new UniTaskCompletionSource();
        var tokenRegistration = token.Register(CancelAwaingOfNextButtonClick);

        var isGameEnded = false;

        SpawnPlayfield();
        SubscribeToMenu();

        while (!isGameEnded)
        {
            ActivateMenu();

            await UniTask.WhenAny(HandlePlayerMove(token), _nextButtonClickCompletionSource.Task);

            if (token.IsCancellationRequested || _nextButtonClickCompletionSource.Task.Status == UniTaskStatus.Succeeded)
            {
                break;
            }

            DeactivateMenu();

            var isFieldNormalized = false;
            while (!isFieldNormalized)
            {
                await DropBlocks(token);

                if (token.IsCancellationRequested)
                {
                    break;
                }

                isFieldNormalized = await DestroyBlocks(token);

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

    private void DespawnPlayfield()
    {
        throw new System.NotImplementedException();
    }

    private async UniTask HandlePlayerMove(CancellationToken token)
    {
        var result = await _swipeHandler.Handle(token);

        if (token.IsCancellationRequested)
        {
            return;
        }

        _swapHandler.Execute(result);

        _positionList.Add(result.FromPosition);
        _positionList.Add(result.ToPosition);

        await _movementHandler.Execute(_positionList, token);

        _positionList.Clear();
    }

    private Task DropBlocks(CancellationToken token)
    {
        throw new System.NotImplementedException();
    }

    private Task<bool> DestroyBlocks(CancellationToken token)
    {
        throw new System.NotImplementedException();
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
}