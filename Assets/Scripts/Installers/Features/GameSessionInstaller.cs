using Elements.DataSource.Data;
using Elements.GameSession.Containers.Implementation;
using Elements.GameSession.Controllers;
using Elements.GameSession.Factories;
using Elements.GameSession.Handlers.Implementation;
using Elements.GameSession.Items;
using Elements.GameSession.LevelProvider;
using Elements.GameSession.LevelSession;
using Elements.GameSession.LevelSession.Controllers;
using Elements.GameSession.LevelSession.Controllers.States.Implementation;
using Elements.GameSession.LevelSession.Controllers.States.Infrastructure;
using Elements.GameSession.LevelSession.Factories;
using Elements.GameSession.Positions;
using Elements.GameSession.Positions.Controllers;
using Elements.GameSession.Positions.Data;
using Elements.GameSession.Positions.Factories;
using Elements.GameSession.Positions.Handlers;
using Elements.GameSession.Positions.Views;
using Elements.GameSession.Views;
using UnityEngine;
using Zenject;

namespace Elements.Installers.Features
{
    public class GameSessionInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _playfieldItemsTransform;
        [SerializeField]
        private GameObject _position;
        [SerializeField]
        private GameObject _firePrefab;
        [SerializeField]
        private GameObject _waterPrefab;

        public override void InstallBindings()
        {
            Container.Bind<GameSessionController>().AsTransient();
            Container.BindInterfacesTo<GameSessionDataHandler>().AsCached();

            Container.BindFactory<LevelSessionFacade, LevelSessionFacadeFactory>().FromSubContainerResolve().ByMethod(InstallLevelSession).AsTransient();
        }

        private void InstallLevelSession(DiContainer subContainer)
        {
            subContainer.Bind<LevelSessionFacade>().AsTransient();

            subContainer.BindFactory<LevelSessionFacade, LevelSessionFacadeFactory>();
            subContainer.BindFactory<LevelSessionSpawnState, LevelSessionSpawnStateFactory>();

            subContainer.BindInterfacesTo<LevelContainer>().AsCached();
            subContainer.Bind<LevelSessionController>().AsCached();

            subContainer.Bind<LevelSessionSpawnState>().AsTransient();
            subContainer.Bind<LevelSessionPlayState>().AsTransient();
            subContainer.Bind<LevelSessionDespawnState>().AsTransient();

            subContainer.BindInterfacesTo<LevelDataProvider>().AsTransient();
            subContainer.BindInterfacesTo<SwipeHandler>().AsTransient();
            subContainer.BindInterfacesTo<SwapHandler>().AsTransient();
            subContainer.BindInterfacesTo<MovementHandler>().AsTransient();
            subContainer.BindInterfacesTo<DropHandler>().AsTransient();
            subContainer.BindInterfacesTo<DestroyHandler>().AsTransient();
            subContainer.BindInterfacesTo<GameEndRulesHandler>().AsTransient();
            subContainer.BindInterfacesTo<PlayfieldSpawnerHelper>().AsTransient();

            subContainer.Bind<PositionsFacade>().FromSubContainerResolve().ByMethod(InstallPositions).AsTransient();
            subContainer.Bind<ItemsFacade>().FromSubContainerResolve().ByMethod(InstallItems).AsTransient();
        }

        private void InstallPositions(DiContainer subContainer)
        {
            subContainer.Bind<IPlayfiedPositioningHandler>().To<PlayfiedPositioningHandler>().AsCached();

            subContainer.Bind<PositionsFacade>().AsTransient();

            subContainer
                .BindFactory<PositionView, PositionViewFactory>()
                .FromComponentInNewPrefab(_position)
                .UnderTransform(_playfieldItemsTransform);

            subContainer.BindFactory<PositionController, PositionControllerFactory>()
                    .FromSubContainerResolve().ByMethod(InstallPosition).AsTransient();
        }

        private void InstallPosition(DiContainer subContainer)
        {
            subContainer.Bind<PositionController>().AsTransient();
            subContainer.Bind<PositionData>().AsTransient();
        }

        private void InstallItems(DiContainer subContainer)
        {
            subContainer.Bind<ItemsFacade>().AsTransient();

            subContainer
                .BindFactory<PositionView, PositionViewFactory>()
                .FromComponentInNewPrefab(_position)
                .UnderTransform(_playfieldItemsTransform);

            subContainer.BindFactory<PositionController, PositionControllerFactory>()
                    .FromSubContainerResolve().ByMethod(InstallPosition).AsTransient();

            subContainer
                .BindFactory<ItemView, FireItemViewFactory>()
                .FromComponentInNewPrefab(_firePrefab)
                .UnderTransform(_playfieldItemsTransform);

            subContainer
                .BindFactory<ItemView, WaterItemViewFactory>()
                .FromComponentInNewPrefab(_waterPrefab)
                .UnderTransform(_playfieldItemsTransform);

            subContainer.BindFactory<ItemType, ItemController, ItemControllerFactory>()
                .FromSubContainerResolve().ByMethod(InstallItem).AsTransient();
        }

        private void InstallItem(DiContainer subContainer, ItemType itemType)
        {
            subContainer.Bind<ItemController>().AsTransient();
            subContainer.BindInstance(itemType);
        }
    }
}