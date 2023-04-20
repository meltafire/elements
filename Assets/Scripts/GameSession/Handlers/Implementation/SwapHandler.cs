using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Data;
using Elements.GameSession.Handlers.Infrastructure;

namespace Elements.GameSession.Handlers.Implementation
{
    public class SwapHandler : ISwapHandler
    {
        private readonly ILevelContainer _levelContainer;

        public void Execute(SwapData swapData)
        {
            var containers = _levelContainer.PositionContainers;
            var fromData = swapData.FromPosition;
            var toData = swapData.ToPosition;

            var fromContainer = containers[fromData.I, fromData.J];
            var toContainer = containers[toData.I, toData.J];

            Execute(fromContainer, toContainer);
        }

        public void Execute(IPositionContainer fromContainer, IPositionContainer toContainer)
        {
            var fromItem = fromContainer.ItemMediator;
            var toItem = toContainer.ItemMediator;

            fromContainer.ItemMediator = toItem;
            toContainer.ItemMediator = fromItem;

            RegisterAtNewPosition(fromContainer);
            RegisterAtNewPosition(toContainer);
        }

        private void RegisterAtNewPosition(IPositionContainer container)
        {
            if (!container.IsEmpty())
            {
                container.ItemMediator.RegisterAtNewPosition(container.PositionMediator);
            }
        }
    }
}