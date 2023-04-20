using Cysharp.Threading.Tasks;
using Elements.DataSource.Data;
using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Data;
using System;
using System.Threading;

namespace Elements.GameSession.Containers.Implementation
{
    public class ItemMediator : IItemMediator
    {
        public ItemType ItemType => throw new NotImplementedException();

        public event Action<PositionData> OnItemSelect;

        public void CreateView(IPositionMediator positionMediator)
        {
            throw new NotImplementedException();
        }

        public UniTask MoveView(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public UniTask PlayDestroyAnimation()
        {
            throw new NotImplementedException();
        }

        public void RegisterAtNewPosition(IPositionMediator positionMediator)
        {
            throw new NotImplementedException();
        }

        public void RemoveView()
        {
            throw new NotImplementedException();
        }
    }
}