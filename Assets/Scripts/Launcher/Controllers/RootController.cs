using Cysharp.Threading.Tasks;
using Elements.Balloons.Controllers;
using Elements.PlayfieldScaler.Handlers.Infrastructure;
using System.Threading;
using UnityEngine;

namespace Elements.Launcher.Controllers
{
    public class RootController
    {
        private readonly IPlayfieldScalerHandler _playfieldScalerHandler;
        private readonly BalloonController _balloonController;

        public RootController(IPlayfieldScalerHandler playfieldScalerHandler, BalloonController balloonController)
        {
            _playfieldScalerHandler = playfieldScalerHandler;
            _balloonController = balloonController;
        }

        public async UniTask Execute(CancellationToken token)
        {
            _playfieldScalerHandler.Scale();

            await _balloonController.Execute(token);

            Application.Quit();
        }
    }
}
