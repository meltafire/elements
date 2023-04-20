using Cysharp.Threading.Tasks;
using Elements.GameSession.Factories;
using System.Threading;

namespace Elements.GameSession.Controllers
{
    public class GameSessionController
    {
        private readonly LevelSessionControllerFactory _levelSessionControllerFactory;

        public GameSessionController(LevelSessionControllerFactory levelSessionControllerFactory)
        {
            _levelSessionControllerFactory = levelSessionControllerFactory;
        }

        public UniTask Execute(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
    }
}