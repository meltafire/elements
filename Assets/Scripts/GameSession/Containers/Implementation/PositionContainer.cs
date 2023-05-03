using Elements.GameSession.Containers.Infrastructure;

namespace Elements.GameSession.Containers.Implementation
{
    public class PositionContainer : IPositionContainer
    {
        private readonly IPositionController _positionMediator;

        private IItemMediator _itemMediator;

        public IPositionController PositionController => _positionMediator;

        public IItemMediator ItemMediator { get => _itemMediator; set => _itemMediator = value; }

        public PositionContainer(IPositionController PositionMediator)
        {
            _positionMediator = PositionMediator;
        }

        public bool IsEmpty()
        {
            return _itemMediator == null;
        }
    }
}