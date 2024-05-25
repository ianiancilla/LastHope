using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour, Controls.ILevelActions
{
    private Controls controls;

    public event Action ShootInputSector1;

    private void Awake()
    {
        controls = new Controls();
        controls.Level.SetCallbacks(this);
        controls.Level.Enable();
    }

    private void OnDestroy()
    {
        controls.Level.Disable();
    }

    public void OnShootSector1(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootInputSector1?.Invoke();
    }
}
