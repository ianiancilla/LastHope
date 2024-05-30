using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    void Start()
    {
        SetSceneVolume();
    }

    private void OnEnable()
    {
        SetSceneVolume();
        OptionsMenu.OnVolumeChanged += SetSceneVolume;
    }

    private void OnDisable()
    {
        OptionsMenu.OnVolumeChanged -= SetSceneVolume;
    }

    public void SetSceneVolume()
    {
        FindFirstObjectByType<AudioSource>().volume = PlayerprefsHelper.GetVolume();
    }
}
