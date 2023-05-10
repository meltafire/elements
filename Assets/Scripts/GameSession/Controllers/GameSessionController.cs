using Cysharp.Threading.Tasks;
using Elements.GameSession.Handlers.Infrastructure;
using Elements.GameSession.LevelSession.Factories;
using System.Threading;

namespace Elements.GameSession.Controllers
{
    public class GameSessionController
    {
        private readonly LevelSessionFacadeFactory _levelSessionFacadeFactory;
        private readonly IGameSessionDataHandler _gameSessionDataHandler;

        public GameSessionController(
            LevelSessionFacadeFactory levelSessionFacadeFactory,
            IGameSessionDataHandler gameSessionDataHandler)
        {
            _levelSessionFacadeFactory = levelSessionFacadeFactory;
            _gameSessionDataHandler = gameSessionDataHandler;
        }

        public async UniTask Execute(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var levelSessionController = _levelSessionFacadeFactory.Create();

                await levelSessionController.Execute(token);

                _gameSessionDataHandler.ItterateToNextLevel();
            }
        }
    }
}