using Cysharp.Threading.Tasks;
using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Data;
using Elements.GameSession.Handlers.Infrastructure;
using System;
using System.Threading;
using UnityEngine;

namespace Elements.GameSession.Handlers.Implementation
{
    public class SwipeHandler : ISwipeHandler
    {
        private readonly ILevelContainer _levelContainer;

        private UniTaskCompletionSource _firstItemSelectionTcs;
        private PositionData _firstItemPositionData;
        private PositionData _finalPositionData;
        private Vector3 _swipeStartPosition;

        public SwipeHandler(ILevelContainer levelContainer)
        {
            _levelContainer = levelContainer;
        }

        public async UniTask<SwapData> Handle(CancellationToken token)
        {
            var isSolutionFound = false;

            while (!isSolutionFound)
            {
                _firstItemSelectionTcs = new UniTaskCompletionSource();
                var tokenRegistration = token.Register(CancelLevelSessionAwaiting);

                foreach (var positionContainer in _levelContainer.PositionContainers)
                {
                    if (!positionContainer.IsEmpty())
                    {
                        positionContainer.ItemController.OnItemSelect += OnItemSelected;
                    }
                }

                await _firstItemSelectionTcs.Task;

                foreach (var positionContainer in _levelContainer.PositionContainers)
                {
                    if (!positionContainer.IsEmpty())
                    {
                        positionContainer.ItemController.OnItemSelect -= OnItemSelected;
                    }
                }

                tokenRegistration.Dispose();

                if (token.IsCancellationRequested)
                {
                    return null;
                }

                _firstItemSelectionTcs = new UniTaskCompletionSource();
                tokenRegistration = token.Register(CancelLevelSessionAwaiting);

                _swipeStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                while(_firstItemSelectionTcs.Task.Status == UniTaskStatus.Pending)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, token);

                    OnUpdated();
                }

                tokenRegistration.Dispose();
                _firstItemSelectionTcs = null;

                if (_finalPositionData != null)
                {
                    isSolutionFound = true;
                }

            }

            return new SwapData(_firstItemPositionData, _finalPositionData);
        }

        private void OnUpdated()
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnSwipeDetermened(null);
            }

            var finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.SqrMagnitude(finalTouchPosition - _swipeStartPosition) > 1)
            {
                var swipeAngle = Math.Atan2(finalTouchPosition.y - _swipeStartPosition.y, finalTouchPosition.x - _swipeStartPosition.x) * 180 / Math.PI;

                var dimensionI = _levelContainer.DimensionI;
                var dimensionJ = _levelContainer.DimensionJ;

                PositionData finalPositionData = null;

                var positionContainers = _levelContainer.PositionContainers;

                if (swipeAngle > -45 && swipeAngle <= 45)
                {
                    //right swipe
                    var finalCoordinateI = _firstItemPositionData.I + 1;
                    if (finalCoordinateI < dimensionI)
                    {
                        finalPositionData = positionContainers[finalCoordinateI, _firstItemPositionData.J].PositionController.Data;
                    }
                }
                else if (swipeAngle > 45 && swipeAngle <= 135)
                {
                    //up swipe
                    var finalCoordinateJ = _firstItemPositionData.J + 1;
                    if (finalCoordinateJ < dimensionJ && !positionContainers[_firstItemPositionData.I, finalCoordinateJ].IsEmpty())
                    {
                        finalPositionData = positionContainers[_firstItemPositionData.I, finalCoordinateJ].PositionController.Data;
                    }
                }
                else if ((swipeAngle > 135 && swipeAngle <= 180) || (swipeAngle >= -180 && swipeAngle <= -135))
                {
                    //left swipe
                    var finalCoordinateI = _firstItemPositionData.I - 1;
                    if (finalCoordinateI >= 0)
                    {
                        finalPositionData = positionContainers[finalCoordinateI, _firstItemPositionData.J].PositionController.Data;
                    }
                }
                else if (swipeAngle < -45 && swipeAngle >= -135)
                {
                    //down swipe
                    var finalCoordinateJ = _firstItemPositionData.J - 1;
                    if (finalCoordinateJ >= 0)
                    {
                        finalPositionData = positionContainers[_firstItemPositionData.I, finalCoordinateJ].PositionController.Data;
                    }
                }

                OnSwipeDetermened(finalPositionData);
            }
        }

        private void OnItemSelected(PositionData data)
        {
            _firstItemPositionData = data;

            CancelLevelSessionAwaiting();
        }

        private void OnSwipeDetermened(PositionData data)
        {
            _finalPositionData = data;

            CancelLevelSessionAwaiting();
        }

        private void CancelLevelSessionAwaiting()
        {
            _firstItemSelectionTcs.TrySetResult();
        }
    }
}