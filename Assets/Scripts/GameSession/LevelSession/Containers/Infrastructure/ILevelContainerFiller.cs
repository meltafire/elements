namespace Elements.GameSession.Containers.Infrastructure
{
    public interface ILevelContainerFiller
    {
        void Fill(IPositionContainer[,] positionContainers, int dimensionI, int dimensionJ);
    }
}