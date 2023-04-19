using Cysharp.Threading.Tasks;
using Elements.GameSession.Data;
using System.Collections.Generic;
using System.Threading;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface IMovementHandler
    {
        UniTask Execute(IEnumerable<PositionData> movementItems, CancellationToken token);
    }
}