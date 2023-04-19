using Cysharp.Threading.Tasks;
using Elements.Balloons.Controllers;
using Elements.GameSession.Controllers;
using Elements.PlayfieldScaler.Handlers.Infrastructure;
using System.Threading;
using UnityEngine;

namespace Elements.Launcher.Controllers
{
    public class RootController
    {
        private readonly IPlayfieldScalerHandler _playfieldScalerHandler;
        private readonly BalloonController _balloonController;
        private readonly GameSessionController _gameSessionController;

        public RootController(
            IPlayfieldScalerHandler playfieldScalerHandler,
            BalloonController balloonController,
            GameSessionController gameSessionController)
        {
            _playfieldScalerHandler = playfieldScalerHandler;
            _balloonController = balloonController;
            _gameSessionController = gameSessionController;
        }

        public async UniTask Execute(CancellationToken token)
        {
            _playfieldScalerHandler.Scale();

            _balloonController.Execute(token).Forget();

            await _gameSessionController.Execute(token);

            Application.Quit();
        }
    }
}
