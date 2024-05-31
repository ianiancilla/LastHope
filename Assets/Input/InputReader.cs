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
    public void OnSector0(InputAction.CallbackContext context)
    {
        ShootEventSector0?.Invoke();
    }

    public void OnSector1(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector1?.Invoke();
    }

    public void OnSector2(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector2?.Invoke();
    }

    public void OnSector3(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector3?.Invoke();
    }

    public void OnSector4(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector4?.Invoke();
    }

    public void OnSector5(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector5?.Invoke();
    }

    public void OnSector6(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector6?.Invoke();
    }

    public void OnSector7(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector7?.Invoke();
    }

    public void OnSector8(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector8?.Invoke();
    }

    public void OnSector9(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector9?.Invoke();
    }

    public void OnSector10(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector10?.Invoke();
    }

    public void OnSector11(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        ShootEventSector11?.Invoke();
    }
}
