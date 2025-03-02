using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollectableEntity : MonoBehaviour
{
    [Inject] StarExplosionParticle.Pool _starExplosionParticlePool;
    [Inject] AudioSystem _audioSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSystem.Play(_audioSystem.GetAudioLibrary().CollectSound);
            _starExplosionParticlePool.Spawn(transform.position);
            gameObject.SetActive(false);
        }
    }
}
