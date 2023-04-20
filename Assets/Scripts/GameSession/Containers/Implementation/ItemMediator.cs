using Cysharp.Threading.Tasks;
using Elements.DataSource.Data;
using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Data;
using Elements.GameSession.Factories;
using Elements.GameSession.Views;
using System;
using System.Threading;

namespace Elements.GameSession.Containers.Implementation
{
    public class ItemMediator : IItemMediator, IDisposable
    {
        private readonly ItemType _itemType;
        private readonly WaterItemViewFactory _waterItemViewFactory;
        private readonly FireItemViewFactory _fireItemViewFactory;

        private ItemView _view;
        private IPositionMediator _positionMediator;

        public event Action<PositionData> OnItemSelect;

        public ItemType ItemType => _itemType;

        public ItemMediator(
            ItemType itemType,
            WaterItemViewFactory waterItemViewFactory,
            FireItemViewFactory fireItemViewFactory)
        {
            _itemType = itemType;
            _waterItemViewFactory = waterItemViewFactory;
            _fireItemViewFactory = fireItemViewFactory;
        }

        public void CreateView()
        {
            if (_view == null)
            {
                _view = SpawnBlock();
            }
        }

        public void RegisterAtNewPosition(IPositionMediator positionMediator)
        {
            Unsubscribe();

            _positionMediator = positionMediator;

            Subcribe();
        }

        public void MoveToPositionImediately()
        {
            _view.SetPosition(_positionMediator.Position);
        }

        public UniTask MoveView(CancellationToken token)
        {
            return _view.MoveToPosition(_positionMediator.Position, token);
        }

        public UniTask PlayDestroyAnimation()
        {
            throw new NotImplementedException();
        }

        public void RemoveView()
        {
            if (_view != null)
            {
                _view.Remove();
            }
        }

        public void Dispose()
        {
            if (_view != null)
            {
                Unsubscribe();
            }
        }

        private void Subcribe()
        {
            _positionMediator.OnItemClick += OnItemSelected;
        }

        private void Unsubscribe()
        {
            if (_positionMediator != null)
            {
                _positionMediator.OnItemClick -= OnItemSelected;
            }
        }

        private void OnItemSelected(PositionData data)
        {
            OnItemSelect?.Invoke(data);
        }

        private ItemView SpawnBlock()
        {
            switch (_itemType)
            {
                case ItemType.Water:
                    return _waterItemViewFactory.Create();

                case ItemType.Fire:
                    return _fireItemViewFactory.Create();

                default:
                    throw new IndexOutOfRangeException();

            }
        }
    }
}