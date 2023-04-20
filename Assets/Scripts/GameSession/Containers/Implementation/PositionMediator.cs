using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Data;
using Elements.GameSession.Factories;
using Elements.GameSession.Handlers.Infrastructure;
using System;

namespace Elements.GameSession.Containers.Implementation
{
    public class PositionMediator : IPositionMediator, IDisposable
    {
        private readonly PositionData _data;
        private readonly IPlayfiedPositioningHandler _positioningHandler;
        private readonly PositionViewFactory _positionViewFactory;

        private PositionView _view;

        public PositionData Data => _data;

        public event Action<PositionData> OnItemClick;

        public PositionMediator(
            int positionI,
            int positionJ,
            PositionViewFactory positionViewFactory,
            IPlayfiedPositioningHandler positioningHandler)
        {
            _positionViewFactory = positionViewFactory;
            _positioningHandler = positioningHandler;

            _data = new PositionData(positionI, positionJ);
        }

        public void Dispose()
        {
            UnsubscribeFromView();
            RemoveView();
        }

        public void CreateView()
        {
            if (_view == null)
            {
                _view = _positionViewFactory.Create();

                AllignView();
                SubcribeToView();
            }
        }

        public void RemoveView()
        {
            if (_view != null)
            {
                _view.Remove();
            }
        }

        private void SubcribeToView()
        {
            _view.OnClick += OnPositionClicked;
        }

        private void UnsubscribeFromView()
        {
            _view.OnClick -= OnPositionClicked;
        }

        private void OnPositionClicked()
        {
            OnItemClick?.Invoke(_data);
        }

        private void AllignView()
        {
            var position = _positioningHandler.GeneratePosition(_data);
            _view.SetPosition(position);
        }
    }
}