using Elements.GameSession.Controllers;
using Elements.GameSession.Factories;
using Zenject;

namespace Elements.Installers.Features
{
    public class GameSessionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSessionController>().AsTransient();

            Container.BindFactory<LevelSessionController, LevelSessionControllerFactory>();
        }
    }
}