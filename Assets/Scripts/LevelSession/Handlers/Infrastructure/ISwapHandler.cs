using System.Collections.Generic;

public interface ISwapHandler
{
    void Execute(IEnumerable<SwapData> swapDatas);
    void Execute(SwapData swapData);
}