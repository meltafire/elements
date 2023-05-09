using Elements.DataSource;
using Elements.DataSource.Data;
using Elements.GameSession.Handlers.Infrastructure;

namespace Elements.GameSession.LevelProvider
{
    public class LevelDataProvider : ILevelDataProvider
    {
        private readonly Levels _levels;
        private readonly IGameSessionDataHandler _gameSessionDataHandler;

        public LevelDataProvider(Levels levels, IGameSessionDataHandler gameSessionDataHandler)
        {
            _levels = levels;
            _gameSessionDataHandler = gameSessionDataHandler;
        }

        public int FieldSizeI => _levels.LevelsCollection[_gameSessionDataHandler.CurrentLevelIndex].FieldSizeI;

        public Item[] Items => _levels.LevelsCollection[_gameSessionDataHandler.CurrentLevelIndex].Items;
    }
}
