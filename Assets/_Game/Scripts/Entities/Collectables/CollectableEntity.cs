using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollectableEntity : MonoBehaviour
{
    [Inject] StarExplosionParticle.Pool _starExplosionParticlePool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _starExplosionParticlePool.Spawn(transform.position);
            gameObject.SetActive(false);
        }
    }
}
