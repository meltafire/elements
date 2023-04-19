using Elements.Balloons.Controllers;
using Elements.Balloons.Data;
using Elements.Balloons.Factories;
using Elements.Balloons.Handlers.Implementation;
using Elements.Balloons.Views;
using UnityEngine;
using Zenject;

namespace Elements.Installers.Features
{
    public class BalloonsInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _parrentTransform;
        [SerializeField]
        public GameObject _prefab;
        [SerializeField]
        private BalloonsSettings _balloonsSettings;

        public override void InstallBindings()
        {
            Container.Bind<BalloonController>().AsTransient();

            Container.BindInterfacesTo<BalloonSpawnerHandler>().AsTransient();

            Container.Bind<BalloonsSettings>().FromInstance(_balloonsSettings);

            Container
                .BindFactory<BalloonData, BalloonView, BalloonViewFactory>()
                .FromMonoPoolableMemoryPool(
                    x => x
                        .WithInitialSize(_balloonsSettings.MaxBalloonOnScreen)
                        .FromComponentInNewPrefab(_prefab)
                        .UnderTransform(_parrentTransform));
        }
    }
}