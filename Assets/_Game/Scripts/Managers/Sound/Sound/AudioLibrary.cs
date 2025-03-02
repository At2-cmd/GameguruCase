using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public AudioGroup PinSound;
    public AudioGroup NoteSound;
    public AudioGroup SuccessSound;
    public AudioGroup FailSound;
}