using Cysharp.Threading.Tasks;
using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Handlers.Infrastructure;
using Elements.GameSession.Positions.Data;
using System.Collections.Generic;
using System.Threading;

namespace Elements.GameSession.Handlers.Implementation
{
    public class MovementHandler : IMovementHandler
    {
        private readonly ILevelContainer _levelContainer;

        private bool _isMovingOngoing;
        private List<UniTask> _movementTasks = new List<UniTask>();

        public MovementHandler(ILevelContainer levelContainer)
        {
            _levelContainer = levelContainer;
        }

        public async UniTask Execute(IEnumerable<PositionData> movementItems, CancellationToken token)
        {
            if (!_isMovingOngoing)
            {
                _isMovingOngoing = true;

                var containers = _levelContainer.PositionContainers;

                foreach (var item in movementItems)
                {
                    var container = containers[item.I, item.J];
                    if (!container.IsEmpty())
                    {
                        _movementTasks.Add(containers[item.I, item.J].ItemController.MoveView(token));
                    }
                }

                await UniTask.WhenAll(_movementTasks);

                _movementTasks.Clear();
                _isMovingOngoing = false;
            }
        }
    }
}