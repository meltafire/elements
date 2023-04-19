using Elements.Launcher.Controllers;
using Zenject;

namespace Elements.Installers.Features
{
    public class LauncherInstaller : Installer<LauncherInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<RootController>().AsTransient();
        }
    }
}
