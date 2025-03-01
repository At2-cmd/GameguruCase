using Zenject;
public class GameManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<EventController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<LevelLoaderController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<UIController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<CameraController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<SliceController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<GroundTileController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<TouchInputController>().FromComponentInHierarchy().AsSingle();
    }
}