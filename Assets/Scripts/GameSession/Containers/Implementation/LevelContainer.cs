using Elements.GameSession.Containers.Infrastructure;

namespace Elements.GameSession.Containers.Implementation
{
    public class LevelContainer : ILevelContainer, ILevelContainerFiller
    {
        private IPositionContainer[,] _positionContainers;
        private int _dimensionI;
        private int _dimensionJ;

        public int DimensionI => _dimensionI;

        public int DimensionJ => _dimensionJ;

        public IPositionContainer[,] PositionContainers => _positionContainers;

        public void Fill(IPositionContainer[,] positionContainers, int dimensionI, int dimensionJ)
        {
            _positionContainers = positionContainers;
            _dimensionI = dimensionI;
            _dimensionJ = dimensionJ;
        }
    }
}