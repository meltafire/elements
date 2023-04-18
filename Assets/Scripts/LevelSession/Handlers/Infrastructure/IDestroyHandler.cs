using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public interface IDestroyHandler
{
    UniTask<IEnumerable<PositionData>> TryDestroyItems(IEnumerable<int> rowsForChec, IEnumerable<int> columnsForCheck, CancellationToken token);
}