using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private StarExplosionParticle starParticlePrefab;
    [SerializeField] private GroundTile groundTilePrefab;
    public override void InstallBindings()
    {
        
        Container.BindMemoryPool<StarExplosionParticle, StarExplosionParticle.Pool>()
                .WithInitialSize(5)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(starParticlePrefab)
                .UnderTransformGroup("StarExplosionParticles");
        
        Container.BindMemoryPool<GroundTile, GroundTile.Pool>()
                .WithInitialSize(5)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(groundTilePrefab)
                .UnderTransformGroup("GroundTiles");
    }
}