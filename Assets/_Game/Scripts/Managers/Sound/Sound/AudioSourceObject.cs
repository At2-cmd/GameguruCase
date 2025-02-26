using UnityEngine;
using Zenject;

public class AudioSourceObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
    public class Pool : MemoryPool<AudioSourceObject>
    {
        protected override void OnCreated(AudioSourceObject item)
        {

        }
    }
}
