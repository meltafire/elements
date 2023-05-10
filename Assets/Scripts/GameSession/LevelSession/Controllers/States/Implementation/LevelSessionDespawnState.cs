using Cysharp.Threading.Tasks;
using Elements.GameSession.Handlers.Infrastructure;
using Elements.GameSession.LevelSession.Controllers.States.Infrastructure;
using System.Threading;

namespace Elements.GameSession.LevelSession.Controllers.States.Implementation
{
    public class LevelSessionDespawnState : ILevelSessionState
    {
        private readonly IPlayfieldSpawnerHelper _playfieldSpawnerHelper;

        public LevelSessionDespawnState(IPlayfieldSpawnerHelper playfieldSpawnerHelper)
        {
            _playfieldSpawnerHelper = playfieldSpawnerHelper;
        }

        public UniTask Execute(LevelSessionController contextController, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                DespawnPlayfield();
            }

            contextController.ChangeState(null);

            return UniTask.CompletedTask;
        }

        private void DespawnPlayfield()
        {
            _playfieldSpawnerHelper.Despawn();
        }
    }
}
