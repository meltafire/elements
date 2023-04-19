using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Elements.Launcher.Controllers
{
    public class RootController
    {
        public async UniTask Execute(CancellationToken token)
        {
            Application.Quit();
        }
    }
}
