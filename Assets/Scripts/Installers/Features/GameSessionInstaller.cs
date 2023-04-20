using Elements.DataSource.Data;
using Elements.GameSession.Containers.Implementation;
using Elements.GameSession.Controllers;
using Elements.GameSession.Factories;
using Elements.GameSession.Handlers.Implementation;
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

            Container.BindFactory<LevelSessionController, LevelSessionControllerFactory>();

            Container.BindInterfacesTo<GameSessionDataHandler>().AsTransient();
            Container.BindInterfacesTo<SwipeHandler>().AsTransient();
            Container.BindInterfacesTo<SwapHandler>().AsTransient();
            Container.BindInterfacesTo<MovementHandler>().AsTransient();
            Container.BindInterfacesTo<DropHandler>().AsTransient();
            Container.BindInterfacesTo<DestroyHandler>().AsTransient();
            Container.BindInterfacesTo<PlayfieldSpawnerHelper>().AsTransient();

            Container.BindInterfacesTo<GameEndRulesHandler>().AsCached();

            Container.BindFactory<int, int, PositionMediator, PositionMediatorFactory>();
            Container.BindFactory<ItemType, ItemMediator, ItemMediatorFactory>();

            Container
                .BindFactory<PositionView, PositionViewFactory>()
                .FromComponentInNewPrefab(_position)
                .UnderTransform(_playfieldItemsTransform);

            Container
                .BindFactory<ItemView, FireItemViewFactory>()
                .FromComponentInNewPrefab(_firePrefab)
                .UnderTransform(_playfieldItemsTransform);

            Container
                .BindFactory<ItemView, WaterItemViewFactory>()
                .FromComponentInNewPrefab(_waterPrefab)
                .UnderTransform(_playfieldItemsTransform);
        }
    }
}