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
        UniTask PlayDestroyAnimation(CancellationToken token);

        void CreateView();
        void RegisterAtNewPosition(IPositionController positionController);
        void MoveToPositionImediately();
        void RemoveView();
    }
}