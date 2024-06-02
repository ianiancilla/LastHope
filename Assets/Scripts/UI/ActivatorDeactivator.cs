using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDeactivator : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToActivate;
    [SerializeField] GameObject[] objectsToDeactivate;
    [SerializeField] bool toggleActivationOnEnableDisable = false; //used for tutorial panels

    private void OnEnable()
    {
        if (!toggleActivationOnEnableDisable) { return; }
        ActivateObjects();
        DeactivateObjects();
    }
    private void OnDisable()
    {
        if (!toggleActivationOnEnableDisable) { return; }

        // deactivate objects
        foreach (var obj in objectsToActivate)
        {
            if (obj != null) { continue; }
            obj.SetActive(true);
        }

        // reactivate objects
        foreach (var obj in objectsToDeactivate)
        {
            if (obj != null) { continue; }
            obj.SetActive(false);
        }
    }

    public void ActivateObjects()
    {
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    public void DeactivateObjects()
    {
        foreach(var obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }


}
