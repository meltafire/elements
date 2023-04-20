using Elements.GameSession.Controllers;
using Elements.GameSession.Factories;
using Elements.GameSession.Handlers.Implementation;
using Zenject;

namespace Elements.Installers.Features
{
    public class GameSessionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSessionController>().AsTransient();

            Container.BindFactory<LevelSessionController, LevelSessionControllerFactory>();

            Container.BindInterfacesTo<GameSessionDataHandler>().AsTransient();
            Container.BindInterfacesTo<SwipeHandler>().AsTransient();
        }
    }
}