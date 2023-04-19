using Cysharp.Threading.Tasks;
using Elements.Launcher.Controllers;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Elements.Launcher
{
    public class LogicEntryPoint : MonoBehaviour
    {
        [Inject]
        private RootController _rootController;

        private CancellationTokenSource _quitTokenSource;

        private void Awake()
        {
            _quitTokenSource = new CancellationTokenSource();
        }

        private void Start()
        {
            _rootController.Execute(_quitTokenSource.Token).Forget();
        }

        private void OnDestroy()
        {
            _quitTokenSource?.Cancel();
        }
    }
}
