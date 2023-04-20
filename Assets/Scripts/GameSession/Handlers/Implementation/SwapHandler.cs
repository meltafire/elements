using Elements.GameSession.Containers.Infrasrtucture;
using Elements.GameSession.Data;
using Elements.GameSession.Handlers.Infrastructure;
using System.Collections.Generic;

namespace Elements.GameSession.Handlers.Implementation
{
    public class SwapHandler : ISwapHandler
    {
        private ILevelContainer _levelContainer;

        public void Execute(IEnumerable<SwapData> swapDatas)
        {
            foreach (var data in swapDatas)
            {
                Execute(data);
            }
        }

        public void Execute(SwapData swapData)
        {
            var containers = _levelContainer.PositionContainers;
            var fromData = swapData.FromPosition;
            var toData = swapData.ToPosition;

            var fromContainer = containers[fromData.I, fromData.J];
            var toContainer = containers[toData.I, toData.J];

            var fromItem = fromContainer.ItemMediator;
            var toItem = toContainer.ItemMediator;

            fromContainer.ItemMediator = toItem;
            toContainer.ItemMediator = fromItem;

            RegisterAtNewPosition(fromContainer);
            RegisterAtNewPosition(toContainer);
        }

        private static void RegisterAtNewPosition(IPositionContainer container)
        {
            if (!container.IsEmpty())
            {
                container.ItemMediator.RegisterAtNewPosition(container.PositionMediator);
            }
        }
    }
}