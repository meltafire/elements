using Cysharp.Threading.Tasks;
using System.Threading;

public interface ISwipeHandler
{
    UniTask<SwapData> Handle(CancellationToken token);
}
