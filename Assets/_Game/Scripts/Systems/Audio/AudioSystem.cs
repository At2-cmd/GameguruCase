using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class AudioSystem : IInitializable
{
    [Inject(Id = "audioLibrary")]
    private AudioLibrary _audioLibrary;
    private static Dictionary<AudioGroup, float> _playtimes = new Dictionary<AudioGroup, float>();
    private AudioSourcePool _pool;

    [Inject]
    public void Constructor(AudioSourcePool pool)
    {
        _pool = pool;
    }
    public void Initialize()
    {
        SetSoundStatus();
    }

    public void ChangeMuteStatus()
    {
        SetSoundStatus();
    }

    private void SetSoundStatus()
    {

    }
    public void Play(AudioGroup audioGroup)
    {
        if (audioGroup.CooldownDuration > 0 && _playtimes.TryGetValue(audioGroup, out float playtime))
        {
            if (Time.time - playtime < audioGroup.CooldownDuration)
                return;
        }

        _playtimes[audioGroup] = Time.time;
        PlayAudio(audioGroup.GetClip(), audioGroup.GetVolume(), audioGroup.GetPitch());
    }

    private void PlayAudio(AudioClip clip, float volume, float pitch)
    {
        var soundPoolObject = _pool.SpawnAudioSource();
        soundPoolObject.AudioSource.volume = volume;
        soundPoolObject.AudioSource.pitch = pitch;
        soundPoolObject.AudioSource.clip = clip;
        soundPoolObject.AudioSource.Play();
        DOTween.Sequence()
            .AppendInterval(clip.length + Time.deltaTime)
            .OnComplete(() =>
            {
                _pool.DeSpawnAudioSource(soundPoolObject);
            });
    }

    public AudioLibrary GetAudioLibrary()
    {
        return _audioLibrary;
    }
}
