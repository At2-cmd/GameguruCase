using UnityEngine;
using Zenject;

public class StarExplosionParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem sparkleParticle;
    private Pool _pool;

    private void Initialize()
    {
        var main = sparkleParticle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void Play(Vector3 pos)
    {
        transform.position = pos;
        sparkleParticle.Play();
    }

    private void Despawn()
    {
        if (!gameObject.activeSelf) return;
        _pool.Despawn(this);
    }

    private void SetPool(Pool pool)
    {
        _pool = pool;
    }

    private void OnParticleSystemStopped()
    {
        Despawn();
    }

    public class Pool : MonoMemoryPool<Vector3, StarExplosionParticle>
    {
        protected override void OnCreated(StarExplosionParticle item)
        {
            base.OnCreated(item);
            item.SetPool(this);
            item.Initialize();
        }

        protected override void Reinitialize(Vector3 pos, StarExplosionParticle item)
        {
            base.Reinitialize(pos, item);
            item.Play(pos);
        }
    }
}
