using _GameData.Scripts.Managers;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
/*
-------------------- HOW TO USE ------------------//
[Inject] private AudioSystem audioSystem;
audioSystem.Play(audioSystem.GetAudioLibrary().TestSound);

Create AudioGroup Scriptable Object for eacy of your sound or sound groups, and then, assign it
to AudioLibrary scriptable object to be able to call. You can also adjust min-max volume, min-max pitch, incremental pitch settings via group SOs.
-------------------- HOW TO USE ------------------//
*/
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
        //if (!SaveManager.GetBool(SaveManager.soundToggleStatus))
        //    return;

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
