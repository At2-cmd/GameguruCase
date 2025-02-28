using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private StarExplosionParticle starParticlePrefab;
    public override void InstallBindings()
    {
        
        Container.BindMemoryPool<StarExplosionParticle, StarExplosionParticle.Pool>()
                .WithInitialSize(5)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(starParticlePrefab)
                .UnderTransformGroup("StarExplosionParticles");
    }
}