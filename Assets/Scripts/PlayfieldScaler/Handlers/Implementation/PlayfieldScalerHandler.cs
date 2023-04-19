using Elements.PlayfieldScaler.Handlers.Infrastructure;
using Elements.PlayfieldScaler.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Elements.PlayfieldScaler.Handlers.Implementation
{
    public class PlayfieldScalerHandler : IPlayfieldScalerHandler
    {
        private readonly CanvasScaler _canvasScaler;
        private readonly PlayfieldScalerView _view;

        public PlayfieldScalerHandler(CanvasScaler canvasScaler, PlayfieldScalerView view)
        {
            _canvasScaler = canvasScaler;
            _view = view;
        }

        public void Scale()
        {
            var referenceResolution = _canvasScaler.referenceResolution;
            var referenceAspect = referenceResolution.x / referenceResolution.y;
            var camera = Camera.main;
            var differenceAtAspect = camera.aspect / referenceAspect;

            var isCameraFrustrumNarrowerThenReference = differenceAtAspect < 1;
            if (isCameraFrustrumNarrowerThenReference)
            {
                var localScale = _view.LocalScale;

                _view.SetLocalScale(
                    new Vector3(
                        localScale.x * differenceAtAspect,
                        localScale.y * differenceAtAspect,
                        1f)
                    );
            }
        }
    }
}