using Cysharp.Threading.Tasks;
using Elements.DataSource.Data;
using Elements.GameSession.Data;
using System;
using System.Threading;

namespace Elements.GameSession.Containers.Infrastructure
{
    public interface IItemMediator
    {
        event Action<PositionData> OnItemSelect;

        ItemType ItemType { get; }

        UniTask MoveView(CancellationToken token);
        UniTask PlayDestroyAnimation();

        void CreateView();
        void RegisterAtNewPosition(IPositionMediator positionMediator);
        void MoveToPositionImediately();
        void RemoveView();
    }
}