using Cysharp.Threading.Tasks;
using Elements.DataSource.Data;
using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Data;
using Elements.GameSession.Handlers.Infrastructure;
using System.Collections.Generic;
using System.Threading;

namespace Elements.GameSession.Handlers.Implementation
{
    public class DestroyHandler : IDestroyHandler
    {
        private const int ItemsToMatchCount = 3;

        private readonly ILevelContainer _levelContainer;
        private readonly List<PositionData> _interactedItems;

        public DestroyHandler(ILevelContainer levelContainer)
        {
            _levelContainer = levelContainer;
            _interactedItems = new List<PositionData>();
        }

        public async UniTask<IEnumerable<PositionData>> TryDestroyItems(CancellationToken token)
        {
            _interactedItems.Clear();

            var positionsToClear = new List<IPositionContainer>();
            var positionContainers = _levelContainer.PositionContainers;

            CheckRows(positionsToClear, positionContainers);
            CheckColumns(positionsToClear, positionContainers);

            var positionsToClearCount = positionsToClear.Count;
            if (positionsToClearCount != 0)
            {
                var animationTasks = new UniTask[positionsToClearCount];

                for (int i = 0; i < positionsToClearCount; i++)
                {
                    animationTasks[i] = positionsToClear[i].ItemController.PlayDestroyAnimation(token);
                }

                await UniTask.WhenAll(animationTasks);

                for (int i = 0; i < positionsToClearCount; i++)
                {
                    var positionToClear = positionsToClear[i];

                    positionToClear.ItemController.RemoveView();
                    positionToClear.ItemController = null;

                    _interactedItems.Add(positionToClear.PositionController.Data);
                }

                return _interactedItems;
            }

            return _interactedItems;
        }

        private void CheckRows(List<IPositionContainer> positionsToClear, IPositionContainer[,] positionContainers)
        {
            for(var rowNumber = 0; rowNumber < _levelContainer.DimensionJ; rowNumber++)
            {
                var sameTypeCounter = 0;
                var lastEncounteredType = ItemType.Empty;

                for (var i = 0; i < _levelContainer.DimensionI; i++)
                {
                    var positionContainer = positionContainers[i, rowNumber];
                    if (positionContainer.IsEmpty())
                    {
                        MarkItemsOfRowForDestroy(sameTypeCounter, positionContainers, rowNumber, i - 1, positionsToClear);

                        lastEncounteredType = ItemType.Empty;
                        sameTypeCounter = 0;
                    }
                    else
                    {
                        var encounteredItemType = positionContainer.ItemController.ItemType;

                        if (encounteredItemType == lastEncounteredType)
                        {
                            sameTypeCounter++;

                            if (i == _levelContainer.DimensionI - 1)
                            {
                                MarkItemsOfRowForDestroy(sameTypeCounter, positionContainers, rowNumber, i, positionsToClear);
                            }
                        }
                        else
                        {
                            MarkItemsOfRowForDestroy(sameTypeCounter, positionContainers, rowNumber, i - 1, positionsToClear);

                            lastEncounteredType = encounteredItemType;
                            sameTypeCounter = 1;
                        }

                    }

                }

            }
        }

        private void CheckColumns(List<IPositionContainer> positionsToClear, IPositionContainer[,] positionContainers)
        {
            for(var columnNumber = 0; columnNumber < _levelContainer.DimensionI; columnNumber++)
            {
                var sameTypeCounter = 0;
                var lastEncounteredType = ItemType.Empty;

                for (var j = 0; j < _levelContainer.DimensionJ; j++)
                {
                    var positionContainer = positionContainers[columnNumber, j];
                    if (positionContainer.IsEmpty())
                    {
                        MarkItemsOfColumnForDestroy(sameTypeCounter, positionContainers, columnNumber, j - 1, positionsToClear);

                        lastEncounteredType = ItemType.Empty;
                        sameTypeCounter = 0;
                    }
                    else
                    {
                        var encounteredItemType = positionContainer.ItemController.ItemType;

                        if (encounteredItemType == lastEncounteredType)
                        {
                            sameTypeCounter++;

                            if (j == _levelContainer.DimensionJ - 1)
                            {
                                MarkItemsOfColumnForDestroy(sameTypeCounter, positionContainers, columnNumber, j, positionsToClear);
                            }
                        }
                        else
                        {
                            MarkItemsOfColumnForDestroy(sameTypeCounter, positionContainers, columnNumber, j - 1, positionsToClear);

                            lastEncounteredType = encounteredItemType;
                            sameTypeCounter = 1;
                        }

                    }

                }

            }
        }

        private void MarkItemsOfRowForDestroy(int sameTypeCounter, IPositionContainer[,] positionContainers, int rowNumber, int startFromIndex, List<IPositionContainer> positionsToClear)
        {
            if (CanBeDestroyed(sameTypeCounter))
            {
                for (var counter = 0; counter < sameTypeCounter; counter++)
                {
                    var positionContainer = positionContainers[startFromIndex - counter, rowNumber];
                    TryAddPositionForDestroy(positionsToClear, positionContainer);

                }

            }

        }

        private void MarkItemsOfColumnForDestroy(int sameTypeCounter, IPositionContainer[,] positionContainers, int columnNumber, int startFromIndex, List<IPositionContainer> positionsToClear)
        {
            if (CanBeDestroyed(sameTypeCounter))
            {
                for (var counter = 0; counter < sameTypeCounter; counter++)
                {
                    var positionContainer = positionContainers[columnNumber, startFromIndex - counter];
                    TryAddPositionForDestroy(positionsToClear, positionContainer);

                }

            }

        }

        private static void TryAddPositionForDestroy(List<IPositionContainer> positionsToClear, IPositionContainer positionContainer)
        {
            if (!positionsToClear.Contains(positionContainer))
            {
                positionsToClear.Add(positionContainer);
            }
        }

        private bool CanBeDestroyed(int sameTypeCounter)
        {
            return sameTypeCounter >= ItemsToMatchCount;
        }
    }
}