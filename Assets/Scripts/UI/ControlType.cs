using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlType : MonoBehaviour
{
    [SerializeField] private Toggle GamePadToggle;
    [SerializeField] private Toggle QwertyToggle;
    [SerializeField] private Toggle ColemakToggle;
    [SerializeField] private Toggle CustomToggle;


    private void Start()
    {
        SetToggleToPlayerPref();
    }

    private void SetToggleToPlayerPref()
    {
        if (PlayerprefsHelper.GetActiveBinding() == 1)
        {
            GamePadToggle.isOn = true;
            return;
        }
        
        switch (PlayerprefsHelper.GetActiveKeyboardType())
        {
            case 1:
                ColemakToggle.isOn = true;
                break;
            case 2:
                CustomToggle.isOn = true;
                break;
            default:
                QwertyToggle.isOn = true;
                break;
        }
    }

    // 0 or 1, 0 for showing kb controls, 1 for gamepad
    public void SetActiveBinding(int binding)
    {
        PlayerprefsHelper.SetActiveBinding(binding);
    }

    // 0 for QWERTY, 1 for Colemak, 2 for custom
    public void SetActiveKeyboardType(int keyboardType)
    {
        PlayerprefsHelper.SetActiveKeyboardType(keyboardType);
    }

    //// DEBUG ONLY!!!
    //private void Start()
    //{
    //    PlayerPrefs.DeleteAll();
    //    Debug.Log("deleted all prefs");
    //}
}
