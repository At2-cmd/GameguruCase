using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private StarExplosionParticle starParticlePrefab;
    [SerializeField] private GroundTile groundTilePrefab;
    [SerializeField] private PerfectMatchFeedback perfectMatchFeedbackPrefab;
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
        
        Container.BindMemoryPool<PerfectMatchFeedback, PerfectMatchFeedback.Pool>()
                .WithInitialSize(3)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(perfectMatchFeedbackPrefab)
                .UnderTransformGroup("PerfectMatchFeedbackPrefabs");
    }
}