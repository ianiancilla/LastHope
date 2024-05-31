using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDeactivator : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToActivate;
    [SerializeField] GameObject[] objectsToDeactivate;

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
            obj?.SetActive(false);
        }
    }
}
