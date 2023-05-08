using Cysharp.Threading.Tasks;
using Elements.GameSession.Data;
using System.Threading;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface ISwipeHandler
    {
        UniTask<SwapData> Handle(CancellationToken token);
    }
}
