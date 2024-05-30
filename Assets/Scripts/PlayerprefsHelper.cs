using UnityEngine;

public static class PlayerprefsHelper
{
    private const string VOLUME = "volume";

    public static void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat(VOLUME, volume);
    }

    public static float GetVolume()
    {
        return PlayerPrefs.GetFloat(VOLUME, 0.5f);
    }
}
