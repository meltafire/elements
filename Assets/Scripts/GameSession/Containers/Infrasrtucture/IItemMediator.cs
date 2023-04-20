using Cysharp.Threading.Tasks;
using Elements.GameSession.Data;
using System;
using System.Threading;

namespace Elements.GameSession.Containers.Infrasrtucture
{
    public interface IItemMediator
    {
        event Action<PositionData> OnItemSelect;

        ItemType ItemType { get; }

        UniTask MoveView(CancellationToken token);
        UniTask PlayDestroyAnimation();

        void RegisterAtNewPosition(IPositionMediator positionMediator);
        void RemoveView();
    }
}