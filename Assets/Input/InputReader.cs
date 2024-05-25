using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class InputReader : MonoBehaviour, Controls.ILevelActions
{
    private Controls controls;

    public UnityEvent ShootEventSector0;
    public UnityEvent ShootEventSector1;
    public UnityEvent ShootEventSector2;
    public UnityEvent ShootEventSector3;
    public UnityEvent ShootEventSector4;
    public UnityEvent ShootEventSector5;
    public UnityEvent ShootEventSector6;
    public UnityEvent ShootEventSector7;
    public UnityEvent ShootEventSector8;
    public UnityEvent ShootEventSector9;
    public UnityEvent ShootEventSector10;
    public UnityEvent ShootEventSector11;

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
    public void OnShootSector0(InputAction.CallbackContext context)
    {
        ShootEventSector0?.Invoke();
    }

    public void OnShootSector1(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector1?.Invoke();
    }

    public void OnShootSector2(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector2?.Invoke();
    }

    public void OnShootSector3(InputAction.CallbackContext context)
    {
        ShootEventSector3?.Invoke();
    }
}
