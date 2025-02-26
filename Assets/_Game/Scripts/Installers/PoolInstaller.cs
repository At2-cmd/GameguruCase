using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private SamplePoolElement samplePoolElement;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<SamplePoolElement, SamplePoolElement.Pool>()
                .WithInitialSize(10)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(samplePoolElement)
                .UnderTransformGroup("SamplePoolElements");
    }
}