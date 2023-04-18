using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public interface IMovementHandler
{
    UniTask Execute(List<PositionData> movementItems, CancellationToken token);
}