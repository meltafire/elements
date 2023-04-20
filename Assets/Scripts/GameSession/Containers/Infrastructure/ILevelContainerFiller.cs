namespace Elements.GameSession.Containers.Infrastructure
{
    public interface ILevelContainerFiller
    {
        void Fill(IPositionContainer[,] itemContainer, int dimensionI, int dimensionJ);
    }
}