using Elements.GameSession.Containers.Infrastructure;

namespace Elements.GameSession.Containers.Implementation
{
    public class PositionContainer : IPositionContainer
    {
        private readonly IPositionMediator _positionMediator;

        private IItemMediator _itemMediator;

        public IPositionMediator PositionMediator => _positionMediator;

        public IItemMediator ItemMediator { get => _itemMediator; set => _itemMediator = value; }

        public PositionContainer(IPositionMediator PositionMediator)
        {
            _positionMediator = PositionMediator;
        }

        public bool IsEmpty()
        {
            return _itemMediator == null;
        }
    }
}