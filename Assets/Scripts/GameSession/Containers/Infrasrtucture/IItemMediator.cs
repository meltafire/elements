using Cysharp.Threading.Tasks;
using Elements.GameSession.Data;
using System;
using System.Threading;

namespace Elements.GameSession.Containers.Infrasrtucture
{
    public interface IItemMediator
    {
        event Action<PositionData> OnItemSelect;

        UniTask MoveView(CancellationToken token);

        void RegisterAtNewPosition(IPositionMediator positionMediator);
    }
}