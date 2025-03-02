using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public AudioGroup CollectSound;
    public AudioGroup NoteSound;
    public AudioGroup SuccessSound;
    public AudioGroup FailSound;
    public AudioGroup SliceSound;
}