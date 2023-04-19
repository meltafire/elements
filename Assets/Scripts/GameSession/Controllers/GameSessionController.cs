using Cysharp.Threading.Tasks;
using Elements.GameSession.Factories;
using System.Threading;

namespace Elements.GameSession.Controllers
{
    public class GameSessionController
    {
        private readonly LevelSessionControllerFactory _levelSessionControllerFactory;

        public async UniTask Execute(CancellationToken token)
        {

        }
    }
}