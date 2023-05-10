using Cysharp.Threading.Tasks;
using Elements.GameSession.Handlers.Infrastructure;
using Elements.GameSession.LevelSession.Controllers.States.Infrastructure;
using System.Threading;

namespace Elements.GameSession.LevelSession.Controllers.States.Implementation
{
    public class LevelSessionSpawnState : ILevelSessionState
    {
        private readonly IPlayfieldSpawnerHelper _playfieldSpawnerHelper;
        private readonly LevelSessionPlayState _levelSessionPlayState;

        public LevelSessionSpawnState(
            IPlayfieldSpawnerHelper playfieldSpawnerHelper,
            LevelSessionPlayState levelSessionPlayState)
        {
            _playfieldSpawnerHelper = playfieldSpawnerHelper;
            _levelSessionPlayState = levelSessionPlayState;
        }

        public UniTask Execute(LevelSessionController contextController, CancellationToken token)
        {
            SpawnPlayfield();

            contextController.ChangeState(_levelSessionPlayState);

            return UniTask.CompletedTask;
        }

        private void SpawnPlayfield()
        {
            _playfieldSpawnerHelper.Spawn();
        }
    }
}
