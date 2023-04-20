using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Data;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface ISwapHandler
    {
        void Execute(SwapData swapData);
        void Execute(IPositionContainer fromContainer, IPositionContainer toContainer);
    }
}