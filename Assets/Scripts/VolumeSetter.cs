using System;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;
    void Start()
    {
        SetBGMVolume();
        SetSFXVolume();
    }

    private void OnEnable()
    {
        SetBGMVolume();
        SetSFXVolume();
        OptionsMenu.OnBGMVolumeChanged += SetBGMVolume;
        OptionsMenu.OnSFXVolumeChanged += SetSFXVolume;
        SceneManager.OnVolumeChanged += OnSceneVolumeChanged;
        LevelSceneManager.OnVolumeChanged += OnSceneVolumeChanged;
    }

    private void OnDisable()
    {
        OptionsMenu.OnBGMVolumeChanged -= SetBGMVolume;
        OptionsMenu.OnSFXVolumeChanged -= SetSFXVolume;
        SceneManager.OnVolumeChanged -= OnSceneVolumeChanged;
        LevelSceneManager.OnVolumeChanged -= OnSceneVolumeChanged;
    }

    private void OnSceneVolumeChanged(float volumeMultiplier)
    {
        BGMSource.volume = PlayerprefsHelper.GetBGMVolume() * volumeMultiplier;
        SFXSource.volume = PlayerprefsHelper.GetSFXVolume() * volumeMultiplier;
    }

    public void SetBGMVolume()
    {
        BGMSource.volume = PlayerprefsHelper.GetBGMVolume();
    }
    public void SetSFXVolume()
    {
        SFXSource.volume = PlayerprefsHelper.GetSFXVolume();
    }

}
