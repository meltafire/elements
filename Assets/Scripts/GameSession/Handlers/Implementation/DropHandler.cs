using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Data;
using Elements.GameSession.Handlers.Infrastructure;
using System.Collections.Generic;

namespace Elements.GameSession.Handlers.Implementation
{
    public class DropHandler : IDropHandler
    {
        private readonly ILevelContainer _levelContainer;
        private readonly ISwapHandler _swapHandler;

        public DropHandler(ILevelContainer levelContainer, ISwapHandler swapHandler)
        {
            _levelContainer = levelContainer;
            _swapHandler = swapHandler;
        }

        public IEnumerable<PositionData> CalculateDropPositions(IEnumerable<int> columnsForCheck)
        {
            var interactedItems = new List<PositionData>();
            var positionContainers = _levelContainer.PositionContainers;

            foreach (var columnNumber in columnsForCheck)
            {
                for (var i = 0; i < _levelContainer.DimensionJ - 1; i++)
                {
                    var swapRequired = false;
                    for (var j = 0; j < _levelContainer.DimensionJ - i - 1; j++)
                    {
                        var fromPositionContainer = positionContainers[columnNumber, j];
                        var toPositionContainer = positionContainers[columnNumber, j + 1];
                        if (fromPositionContainer.IsEmpty() && !toPositionContainer.IsEmpty())
                        {
                            _swapHandler.Execute(fromPositionContainer, toPositionContainer);

                            swapRequired = true;
                        }

                    }

                    if (swapRequired == false)
                    {
                        break;
                    }

                }

                for (var j = 0; j < _levelContainer.DimensionJ; j++)
                {
                    var container = positionContainers[columnNumber, j];
                    if (!container.IsEmpty())
                    {
                        interactedItems.Add(container.PositionController.Data);
                    }
                }

            }

            return interactedItems;
        }
    }
}