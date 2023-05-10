using Cysharp.Threading.Tasks;
using System.Threading;

namespace Elements.GameSession.LevelSession.Controllers.States.Infrastructure
{
    public interface ILevelSessionState
    {
        UniTask Execute(LevelSessionController contextController, CancellationToken token);
    }
}
