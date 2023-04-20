using Elements.GameSession.Containers.Infrasrtucture;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface ILevelContainer
    {
        IPositionContainer[,] PositionContainers { get; }
    }
}