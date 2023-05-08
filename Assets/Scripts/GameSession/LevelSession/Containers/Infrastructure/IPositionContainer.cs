namespace Elements.GameSession.Containers.Infrastructure
{
    public interface IPositionContainer
    {
        IPositionController PositionController { get;}
        IItemController ItemController { get; set; }

        bool IsEmpty();
    }
}