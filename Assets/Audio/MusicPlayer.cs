using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicClip;

    private void Start()
    {
        MusicManager.instance.PlayMusic(musicClip);
    }
}
