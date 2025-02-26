using UnityEngine;
using Zenject;

public class AudioSystemInstaller : MonoInstaller
{
    [SerializeField] private AudioLibrary audioLibrary;
    [SerializeField] private GameObject audioSourcePoolObject;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<AudioSourceObject, AudioSourceObject.Pool>()
            .FromComponentInNewPrefab(audioSourcePoolObject);

        Container.BindInterfacesAndSelfTo<AudioSystem>().AsSingle().NonLazy();

        Container.Bind<AudioLibrary>().WithId("audioLibrary").FromInstance(audioLibrary)
                .WhenInjectedInto<AudioSystem>().NonLazy();

        Container.Bind<AudioSourcePool>().AsSingle();
    }
}
