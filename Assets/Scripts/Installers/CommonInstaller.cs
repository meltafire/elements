using Elements.Installers.Features;
using Zenject;

namespace Elements.Installers
{
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            LauncherInstaller.Install(Container);
        }
    }
}