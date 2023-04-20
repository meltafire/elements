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

        public IEnumerable<PositionData> CalculateDropPositions(IEnumerable<int> columnsForCheck)
        {
            var interactedItems = new List<PositionData>();
            var positionContainers = _levelContainer.PositionContainers;

            foreach (var columnNumber in columnsForCheck)
            {
                for (int i = 0; i < _levelContainer.DimensionI - 1; i++)
                {
                    var swapRequired = false;
                    for (int j = 0; j < _levelContainer.DimensionI - i - 1; j++)
                    {
                        var fromPositionContainer = positionContainers[j, columnNumber];
                        var toPositionContainer = positionContainers[j + 1, columnNumber];
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

                for (int i = 0; i < _levelContainer.DimensionI; i++)
                {
                    var container = positionContainers[i, columnNumber];
                    if (!container.IsEmpty())
                    {
                        interactedItems.Add(container.PositionMediator.Data);
                    }
                }

            }

            return interactedItems;
        }
    }
}