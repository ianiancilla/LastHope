using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class RebindSavingLoading : MonoBehaviour
{
    public InputActionAsset actions;

    public static event Action KeyRebind;

    private void Start()
    {
        LoadRebinds();
    }

    public void OnEnable()
    {
        LoadRebinds();
    }

    public void OnDisable()
    {
        SaveRebinds();
    }

    public void SaveRebinds()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerprefsHelper.SetRebinds(rebinds);
    }

    public void LoadRebinds()
    {
        var rebinds = PlayerprefsHelper.GetRebinds();
        if (!string.IsNullOrEmpty(rebinds))
        {
            Debug.Log("Rebinds loaded from Json.");
            actions.LoadBindingOverridesFromJson(rebinds);
            KeyRebind?.Invoke();
        }
    }
}
