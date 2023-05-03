namespace Elements.GameSession.Containers.Infrastructure
{
    public interface IPositionContainer
    {
        IPositionController PositionController { get;}
        IItemMediator ItemMediator { get; set; }

        bool IsEmpty();
    }
}