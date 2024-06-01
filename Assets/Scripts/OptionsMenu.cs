using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    public static event Action OnVolumeChanged;

    public void OnVolumeSliderChanged()
    {
        PlayerprefsHelper.SetVolume(volumeSlider.value);
        OnVolumeChanged?.Invoke();
    }
}
