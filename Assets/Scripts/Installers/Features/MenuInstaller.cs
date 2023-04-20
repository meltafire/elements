using Elements.Menu.Providers.Implementation;
using Elements.Menu.Providers.Infrastructure;
using UnityEngine;
using Zenject;

namespace Elements.Installers.Features
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField]
        private MenuProviderView _menuProviderView;

        public override void InstallBindings()
        {
            Container.Bind<IMenuProvider>().FromInstance(_menuProviderView);
        }
    }
}