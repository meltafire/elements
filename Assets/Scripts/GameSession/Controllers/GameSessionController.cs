using Cysharp.Threading.Tasks;
using Elements.DataSource;
using Elements.GameSession.Factories;
using System.Threading;
using Zenject;

namespace Elements.GameSession.Controllers
{
    public class GameSessionController
    {
        private readonly Levels _levels;
        private readonly LevelSessionControllerFactory _levelSessionControllerFactory;
        private readonly DiContainer _diContainer;

        public GameSessionController(DiContainer diContainer, Levels levels, LevelSessionControllerFactory levelSessionControllerFactory)
        {
            _diContainer = diContainer;
            _levels = levels;
            _levelSessionControllerFactory = levelSessionControllerFactory;
        }

        public UniTask Execute(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
    }
}