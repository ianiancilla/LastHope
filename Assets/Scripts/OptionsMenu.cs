using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider BGMVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;

    public static event Action OnBGMVolumeChanged;
    public static event Action OnSFXVolumeChanged;


    private void Start()
    {
        BGMVolumeSlider.value = PlayerprefsHelper.GetBGMVolume();
        SFXVolumeSlider.value = PlayerprefsHelper.GetSFXVolume();

    }

    public void OnBGMVolumeSliderChanged()
    {
        PlayerprefsHelper.SetBGMVolume(BGMVolumeSlider.value);
        OnBGMVolumeChanged?.Invoke();
    }
    public void OnSFXVolumeSliderChanged()
    {
        PlayerprefsHelper.SetSFXVolume(SFXVolumeSlider.value);
        OnSFXVolumeChanged?.Invoke();
    }

}
