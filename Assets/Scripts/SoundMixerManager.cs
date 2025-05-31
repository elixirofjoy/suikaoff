using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    public static SoundMixerManager instance;

    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ApplySavedVolumes();
    }

    public void SetMasterVolume(float sliderValue)
    {
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", dB);
        PlayerPrefs.SetFloat("MasterVolume_dB", dB);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MusicVolume", dB);
        PlayerPrefs.SetFloat("MusicVolume_dB", dB);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat("SFXVolume_dB", dB);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    public void ApplySavedVolumes()
    {
        if (!audioMixer) return;

        if (PlayerPrefs.HasKey("MasterVolume_dB"))
            audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume_dB"));

        if (PlayerPrefs.HasKey("MusicVolume_dB"))
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume_dB"));

        if (PlayerPrefs.HasKey("SFXVolume_dB"))
            audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume_dB"));
    }

    public void DebugPrintVolumes()
    {
        float val;
        audioMixer.GetFloat("MasterVolume", out val);
        Debug.Log("MasterVolume dB: " + val);
        audioMixer.GetFloat("MusicVolume", out val);
        Debug.Log("MusicVolume dB: " + val);
        audioMixer.GetFloat("SFXVolume", out val);
        Debug.Log("SFXVolume dB: " + val);
    }
}
