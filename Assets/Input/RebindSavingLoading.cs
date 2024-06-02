using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class RebindSavingLoading : MonoBehaviour
{
    public InputActionAsset actions;

    public static event Action KeyRebind;

    private string rebinds;

    private const string QWERTY = "";
    private const string COLEMAK = "{\"bindings\":[" +
                "{\"action\":\"Level/Sector 0\",\"id\":\"3492962f-232a-4806-b238-42d5fa760bba\",\"path\":\" < Keyboard>/y\",\"interactions\":\"null\",\"processors\":\"null\"}," +
                "{\"action\":\"Level/Sector 5\",\"id\":\"7cb5c7a3-0fd8-467c-abcf-23fbea37be6b\",\"path\":\" < Keyboard>/r\",\"interactions\":\"null\",\"processors\":\"null\"}," +
                "{\"action\":\"Level/Sector 6\",\"id\":\"273161e6-5c7b-44db-b483-6843e6f2d86a\",\"path\":\" < Keyboard>/s\",\"interactions\":\"null\",\"processors\":\"null\"}," +
                "{\"action\":\"Level/Sector 7\",\"id\":\"3656811b-a724-403a-aa17-a585331659b3\",\"path\":\" < Keyboard>/t\",\"interactions\":\"null\",\"processors\":\"null\"}," +
                "{\"action\":\"Level/Sector 10\",\"id\":\"5c67c688-0d3d-43e7-b63c-99aa30fa4dc7\",\"path\":\" < Keyboard>/f\",\"interactions\":\"null\",\"processors\":\"null\"}," +
                "{\"action\":\"Level/Sector 11\",\"id\":\"f2d528fe-a8f7-4c0f-aede-a089e131b948\",\"path\":\" < Keyboard>/p\",\"interactions\":\"null\",\"processors\":\"null\"}]}";

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
        PlayerprefsHelper.SetCustomRebinds(rebinds);
        //Debug.Log(rebinds);
    }

    public void LoadRebinds()
    {
        switch (PlayerprefsHelper.GetActiveKeyboardType())
        {
            case 1:
                rebinds = COLEMAK;
                break;
            case 2:
                rebinds = PlayerprefsHelper.GetCustomRebinds();
                break;
            default:
                rebinds = QWERTY;
                break;
        }
        if (rebinds != null)
        {
            //Debug.Log("Rebinds loaded.");
            actions.LoadBindingOverridesFromJson(rebinds);
            KeyRebind?.Invoke();
        }
    }
}
