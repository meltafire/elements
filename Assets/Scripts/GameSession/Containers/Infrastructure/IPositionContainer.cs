namespace Elements.GameSession.Containers.Infrastructure
{
    public interface IPositionContainer
    {
        IPositionMediator PositionMediator { get;}
        IItemMediator ItemMediator { get; set; }

        bool IsEmpty();
    }
}