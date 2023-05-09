using Elements.DataSource;
using Elements.GameSession.Handlers.Infrastructure;

namespace Elements.GameSession.Handlers.Implementation
{
    public class GameSessionDataHandler : IGameSessionDataHandler
    {
        private readonly int _levelCollectionSize;

        private int _levelNumber;

        public int CurrentLevelIndex => _levelNumber;

        public GameSessionDataHandler(Levels levels)
        {
            _levelCollectionSize = levels.LevelsCollection.Length;
        }

        public void ItterateToNextLevel()
        {
            _levelNumber++;

            _levelNumber = _levelNumber >= _levelCollectionSize ? 0 : _levelNumber;
        }
    }
}