using Elements.Balloons.Views;

namespace Elements.Balloons.Handlers.Infrastructure
{
    public interface IBalloonSpawnerHandler
    {
        void Initialize();
        BalloonView SpawnBalloon(int balloonCount);
    }
}