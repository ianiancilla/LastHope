using UnityEngine;

public static class PlayerprefsHelper
{
    private const string VOLUME = "volume";
    private const string REBINDS = "rebinds";

    public static void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat(VOLUME, volume);
    }

    public static float GetVolume()
    {
        return PlayerPrefs.GetFloat(VOLUME, 0.5f);
    }

    public static void SetRebinds(string rebinds)
    {
        PlayerPrefs.SetString(REBINDS, rebinds);
    }

    public static string GetRebinds()
    {
        return PlayerPrefs.GetString(REBINDS, null);
    }
}
