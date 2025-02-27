using Zenject;
public class GameManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<EventController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<LevelLoaderController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<UIController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<GridController>().FromComponentInHierarchy().AsSingle();
    }
}