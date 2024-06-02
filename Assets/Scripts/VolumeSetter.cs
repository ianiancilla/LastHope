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
    }

    private void OnDisable()
    {
        OptionsMenu.OnBGMVolumeChanged -= SetBGMVolume;
        OptionsMenu.OnSFXVolumeChanged -= SetSFXVolume;
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
