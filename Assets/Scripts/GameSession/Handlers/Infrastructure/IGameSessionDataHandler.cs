namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface IGameSessionDataHandler
    {
        int CurrentLevelIndex { get; }
        void ItterateToNextLevel();
    }
}