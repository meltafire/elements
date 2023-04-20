namespace Elements.GameSession.Containers.Infrasrtucture
{
    public interface IPositionContainer
    {
        IPositionMediator PositionMediator { get; set; }
        IItemMediator ItemMediator { get; set; }

        bool IsEmpty();
    }
}