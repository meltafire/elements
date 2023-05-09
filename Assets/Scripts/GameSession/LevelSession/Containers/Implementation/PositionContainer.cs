using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Positions.Controllers;

namespace Elements.GameSession.Containers.Implementation
{
    public class PositionContainer : IPositionContainer
    {
        private readonly IPositionController _positionMediator;

        private IItemController _itemController;

        public IPositionController PositionController => _positionMediator;

        public IItemController ItemController { get => _itemController; set => _itemController = value; }

        public PositionContainer(IPositionController PositionMediator)
        {
            _positionMediator = PositionMediator;
        }

        public bool IsEmpty()
        {
            return _itemController == null;
        }
    }
}