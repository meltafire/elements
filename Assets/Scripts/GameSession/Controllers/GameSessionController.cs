using Cysharp.Threading.Tasks;
using Elements.GameSession.Factories;
using Elements.GameSession.Handlers.Infrastructure;
using System.Threading;

namespace Elements.GameSession.Controllers
{
    public class GameSessionController
    {
        private readonly LevelSessionControllerFactory _levelSessionControllerFactory;
        private readonly IGameSessionDataHandler _gameSessionDataHandler;

        public GameSessionController(
            LevelSessionControllerFactory levelSessionControllerFactory,
            IGameSessionDataHandler gameSessionDataHandler)
        {
            _levelSessionControllerFactory = levelSessionControllerFactory;
            _gameSessionDataHandler = gameSessionDataHandler;
        }

        public async UniTask Execute(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var levelSessionController = _levelSessionControllerFactory.Create();

                await levelSessionController.Execute(token);

                _gameSessionDataHandler.ItterateToNextLevel();
            }
        }
    }
}