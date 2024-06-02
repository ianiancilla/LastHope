using UnityEngine;

public static class PlayerprefsHelper
{
    private const string BGM_VOLUME = "BGMvolume";
    private const string SFX_VOLUME = "SFXvolume";

    private const string REBINDS = "rebinds";
    private const string ACTIVE_BINDING = "activeBinding"; // 0 or 1, 0 for showing kb controls, 1 for gamepad
    private const string ACTIVE_KB_TYPE = "activeKeyboard"; // 0 for QWERTY, 1 for Colemak, 2 for custom

    public static void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(BGM_VOLUME, volume);
    }
    public static float GetBGMVolume()
    {
        return PlayerPrefs.GetFloat(BGM_VOLUME, 0.5f);
    }

    public static void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFX_VOLUME, volume);
    }
    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME, 0.5f);
    }

    public static void SetCustomRebinds(string rebinds)
    {
        PlayerPrefs.SetString(REBINDS, rebinds);
    }
    public static string GetCustomRebinds()
    {
        return PlayerPrefs.GetString(REBINDS, null);
    }

    public static void SetActiveBinding(int binding)
    {
        if (binding < 0 || binding > 1)
        {
            Debug.LogError("Trying to set a control binding that is out of index!");
            return;
        }
        Debug.Log($"Setting active binding to {binding}");
        PlayerPrefs.SetInt(ACTIVE_BINDING, binding);
    }
    public static int GetActiveBinding()
    {
        return PlayerPrefs.GetInt(ACTIVE_BINDING, 0);
    }

    public static void SetActiveKeyboardType(int kbType)
    {
        if (kbType < 0 || kbType > 2)
        {
            Debug.LogError("Trying to set a kb type that is out of index!");
            return;
        }
        Debug.Log($"Setting active kb to {kbType}");
        PlayerPrefs.SetInt(ACTIVE_KB_TYPE, kbType);
    }
    public static int GetActiveKeyboardType()
    {
        return PlayerPrefs.GetInt(ACTIVE_KB_TYPE, 0);
    }

}
