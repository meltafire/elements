using Elements.PlayfieldScaler.Handlers.Implementation;
using Elements.PlayfieldScaler.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Elements.Installers.Features
{
    public class PlayfieldScalerInstaller : MonoInstaller
    {
        [SerializeField]
        private CanvasScaler _canvasScaler;
        [SerializeField]
        private PlayfieldScalerView _view;

        public override void InstallBindings()
        {
            Container.Bind<CanvasScaler>().FromInstance(_canvasScaler);
            Container.Bind<PlayfieldScalerView>().FromInstance(_view);

            Container.BindInterfacesTo<PlayfieldScalerHandler>().AsTransient();
        }
    }
}