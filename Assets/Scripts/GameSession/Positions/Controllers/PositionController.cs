﻿using Elements.GameSession.LevelProvider;
using Elements.GameSession.Positions.Data;
using Elements.GameSession.Positions.Factories;
using Elements.GameSession.Positions.Handlers;
using Elements.GameSession.Positions.Views;
using System;

namespace Elements.GameSession.Positions.Controllers
{
    public class PositionController : IPositionController, IDisposable
    {
        private readonly PositionData _data;
        private readonly IPlayfiedPositioningHandler _positioningHandler;
        private readonly PositionViewFactory _positionViewFactory;
        private readonly ILevelDataProvider _levelDataSourceProvider;

        private PositionView _view;

        public PositionData Data => _data;
        public UnityEngine.Vector3 Position => _view.Position;

        public event Action<PositionData> OnItemClick;

        public PositionController(
            PositionViewFactory positionViewFactory,
            IPlayfiedPositioningHandler positioningHandler,
            PositionData data,
            ILevelDataProvider levelDataSourceProvider)
        {
            _positionViewFactory = positionViewFactory;
            _positioningHandler = positioningHandler;
            _data = data;
            _levelDataSourceProvider = levelDataSourceProvider;
        }

        public void InitializeData(int i, int j)
        {
            _data.SetData(i, j);
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
                _view.Dispose();
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
            var position = _positioningHandler.GeneratePosition(_levelDataSourceProvider.FieldSizeI, _data);
            _view.SetPosition(position);
        }
    }
}