using Elements.DataSource;
using UnityEngine;
using Zenject;

public class DataSourceInstaller : MonoInstaller
{
    [SerializeField]
    private Levels _levels;

    public override void InstallBindings()
    {
        Container.Bind<Levels>().FromInstance(_levels);
    }
}
