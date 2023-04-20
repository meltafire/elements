using Elements.GameSession.Containers.Infrasrtucture;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface ILevelContainer
    {
        int DimensionI { get; }
        int DimensionJ { get; }

        IPositionContainer[,] PositionContainers { get; }
    }
}