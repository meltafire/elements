using Elements.GameSession.Data;

namespace Elements.GameSession.Containers.Infrastructure
{
    public interface IPositionMediator
    {
        PositionData Data { get; }

        void CreateView();
        void RemoveView();
    }
}