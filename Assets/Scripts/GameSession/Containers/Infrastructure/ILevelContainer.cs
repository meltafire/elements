using Elements.GameSession.Containers.Infrastructure;

namespace Elements.GameSession.Containers.Infrastructure
{
    public interface ILevelContainer
    {
        int DimensionI { get; }
        int DimensionJ { get; }

        IPositionContainer[,] PositionContainers { get; }
    }
}