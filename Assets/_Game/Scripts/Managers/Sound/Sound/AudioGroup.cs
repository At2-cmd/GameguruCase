using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioGroup")]
public class AudioGroup : ScriptableObject
{
    public AudioClip[] AudioClips;
    public float PitchMin;
    public float PitchMax;
    public float VolMin;
    public float VolMax;
    public float CooldownDuration;
    public int IncrementalPitchSteps;
    public float IncrementalPitchCountDown;

    public int SoundIndex;
    private float _lastPlaytime;
    private int _playCount;

    public AudioClip GetClip()
    {
        SoundIndex = (SoundIndex) % AudioClips.Length;
        return AudioClips[SoundIndex];
    }

    public float GetVolume()
    {
        return Random.Range(VolMin, VolMax);
    }

    public float GetPitch()
    {
        if (IncrementalPitchSteps > 1)
        {
            float deltaTime = Time.time - _lastPlaytime;

            if (deltaTime < IncrementalPitchCountDown)
            {
                float deltaPitch = (PitchMax - PitchMin) / (float)IncrementalPitchSteps;
                _lastPlaytime = Time.time;
                _playCount = Mathf.Min(IncrementalPitchSteps, _playCount + 1);
                return PitchMin + (deltaPitch * _playCount);
            }
            else
            {
                _playCount = 0;
                _lastPlaytime = Time.time;
                return PitchMin;
            }
        }

        return Random.Range(PitchMin, PitchMax);
    }
}