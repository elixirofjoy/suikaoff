using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource _musicSourcePrefab;

    private AudioSource currentMusicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (SoundMixerManager.instance != null)
            {
                SoundMixerManager.instance.ApplySavedVolumes();
            }

            if (currentMusicSource == null && _musicSourcePrefab != null)
            {
                // Создаём AudioSource из префаба
                currentMusicSource = Instantiate(_musicSourcePrefab, transform.position, Quaternion.identity, transform);
                currentMusicSource.loop = true;
                currentMusicSource.playOnAwake = false;
                currentMusicSource.spatialBlend = 0f; // 2D-звук
            }
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликат
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip == null || currentMusicSource == null)
        {
            Debug.LogWarning("MusicManager: Missing clip or music source.");
            return;
        }

        if (currentMusicSource.clip == musicClip && currentMusicSource.isPlaying)
            return;

        currentMusicSource.clip = musicClip;
        currentMusicSource.Play();
    }

    public void StopMusic()
    {
        if (currentMusicSource != null)
        {
            currentMusicSource.Stop();
        }
    }
}
