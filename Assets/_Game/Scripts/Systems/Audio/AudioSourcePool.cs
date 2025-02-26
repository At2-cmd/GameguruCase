using UnityEngine;

public class AudioSourcePool
{
    private AudioSourceObject.Pool _pool;

    public AudioSourcePool(AudioSourceObject.Pool pool)
    {
        _pool = pool;
    }
    public AudioSourceObject SpawnAudioSource()
    {
        var source = _pool.Spawn();
        source.gameObject.SetActive(true);
        return source;
    }

    public void DeSpawnAudioSource(AudioSourceObject audioSource)
    {
        audioSource.gameObject.SetActive(false);
        _pool.Despawn(audioSource);
    }

    public int GetPoolSize()
    {
        return _pool.NumTotal;
    }
}
