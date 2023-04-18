using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

public class LevelSesstionController
{
    private readonly IMenuProvider _menuProvider;

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

    private UniTask<object> HandlePlayerMove(CancellationToken token)
    {
        throw new System.NotImplementedException();
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