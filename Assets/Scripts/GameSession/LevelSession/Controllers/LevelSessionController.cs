using Cysharp.Threading.Tasks;
using Elements.GameSession.LevelSession.Controllers.States.Infrastructure;
using System.Threading;

namespace Elements.GameSession.LevelSession.Controllers
{
    public class LevelSessionController
    {
        private ILevelSessionState _state;

        public async UniTask Execute(CancellationToken token)
        {
            while (_state != null)
            {
                await _state.Execute(this, token);
            }
        }

        public void ChangeState(ILevelSessionState state)
        {
            _state = state;
        }
    }
}
