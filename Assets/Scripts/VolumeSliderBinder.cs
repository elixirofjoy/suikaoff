using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderBinder : MonoBehaviour
{
    public enum VolumeType { Master, Music, SFX }

    [SerializeField] private VolumeType volumeType;
    [SerializeField] private Slider slider;

    private bool suppressCallback = false;

    private void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        suppressCallback = true;

        // Устанавливаем стартовое значение из PlayerPrefs
        float savedValue = PlayerPrefs.GetFloat(volumeType.ToString() + "Volume", 1f);
        slider.value = savedValue;

        suppressCallback = false;

        // Подключаем обработчик
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        if (suppressCallback) return;

        switch (volumeType)
        {
            case VolumeType.Master:
                SoundMixerManager.instance.SetMasterVolume(value);
                break;
            case VolumeType.Music:
                SoundMixerManager.instance.SetMusicVolume(value);
                break;
            case VolumeType.SFX:
                SoundMixerManager.instance.SetSFXVolume(value);
                break;
        }
    }
}
