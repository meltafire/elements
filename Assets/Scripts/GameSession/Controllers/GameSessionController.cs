using Cysharp.Threading.Tasks;
using Elements.DataSource;
using Elements.DataSource.Data;
using Elements.GameSession.Containers.Implementation;
using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Factories;
using Elements.GameSession.Handlers.Infrastructure;
using System.Threading;
using Zenject;

namespace Elements.GameSession.Controllers
{
    public class GameSessionController
    {
        private readonly Levels _levels;
        private readonly LevelSessionControllerFactory _levelSessionControllerFactory;
        private readonly DiContainer _diContainer;
        private readonly IGameSessionDataHandler _gameSessionDataHandler;

        public GameSessionController(
            DiContainer diContainer,
            Levels levels,
            LevelSessionControllerFactory levelSessionControllerFactory,
            IGameSessionDataHandler gameSessionDataHandler)
        {
            _diContainer = diContainer;
            _levels = levels;
            _levelSessionControllerFactory = levelSessionControllerFactory;
            _gameSessionDataHandler = gameSessionDataHandler;
        }

        public async UniTask Execute(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var level = _levels.LevelsCollection[_gameSessionDataHandler.CurrentLevelIndex];

                _diContainer.Bind<ILevelDataSourceProvider>().FromInstance(level);

                var levelContainer = new LevelContainer();
                _diContainer.Bind<ILevelContainer>().FromInstance(levelContainer);
                _diContainer.Bind<ILevelContainerFiller>().FromInstance(levelContainer);

                var levelSessionController = _levelSessionControllerFactory.Create();

                await levelSessionController.Execute(token);

                _diContainer.Unbind<ILevelDataSourceProvider>();
                _diContainer.Unbind<ILevelContainer>();
                _diContainer.Unbind<ILevelContainerFiller>();

                _gameSessionDataHandler.ItterateToNextLevel();
            }
        }
    }
}