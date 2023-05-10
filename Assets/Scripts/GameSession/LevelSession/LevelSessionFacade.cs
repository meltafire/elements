using Cysharp.Threading.Tasks;
using Elements.GameSession.LevelSession.Controllers;
using Elements.GameSession.LevelSession.Factories;
using System.Threading;

namespace Elements.GameSession.LevelSession
{
    public class LevelSessionFacade
    {
        private readonly LevelSessionSpawnStateFactory _levelSessionSpawnStateFactory;
        private readonly LevelSessionController _levelSessionController;

        public LevelSessionFacade(LevelSessionController levelSessionController, LevelSessionSpawnStateFactory levelSessionSpawnStateFactory)
        {
            _levelSessionController = levelSessionController;
            _levelSessionSpawnStateFactory = levelSessionSpawnStateFactory;
        }

        public UniTask Execute(CancellationToken token)
        {
            var spawnState = _levelSessionSpawnStateFactory.Create(); 
            _levelSessionController.ChangeState(spawnState);

            return _levelSessionController.Execute(token);
        }
    }
}