using Cysharp.Threading.Tasks;
using Elements.PlayfieldScaler.Handlers.Infrastructure;
using System.Threading;
using UnityEngine;

namespace Elements.Launcher.Controllers
{
    public class RootController
    {
        private readonly IPlayfieldScalerHandler _playfieldScalerHandler;

        public RootController(IPlayfieldScalerHandler playfieldScalerHandler)
        {
            _playfieldScalerHandler = playfieldScalerHandler;
        }

        public async UniTask Execute(CancellationToken token)
        {
            _playfieldScalerHandler.Scale();

            Application.Quit();
        }
    }
}
