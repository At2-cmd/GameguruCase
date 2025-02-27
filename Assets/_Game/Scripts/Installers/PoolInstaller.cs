using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private GridCell gridCell;
    [SerializeField] private StarExplosionParticle starParticlePrefab;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<GridCell, GridCell.Pool>()
                .WithInitialSize(25)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(gridCell)
                .UnderTransformGroup("GridCells");
        
        Container.BindMemoryPool<StarExplosionParticle, StarExplosionParticle.Pool>()
                .WithInitialSize(5)
                .ExpandByDoubling()
                .FromComponentInNewPrefab(starParticlePrefab)
                .UnderTransformGroup("StarExplosionParticles");
    }
}