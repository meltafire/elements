using Elements.GameSession.Data;
using System.Collections.Generic;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface ISwapHandler
    {
        void Execute(IEnumerable<SwapData> swapDatas);
        void Execute(SwapData swapData);
    }
}