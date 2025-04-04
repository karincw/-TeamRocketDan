using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReaderSO : ScriptableObject, IPlayerActions, IUIActions
{

    public event Action LeftClickEvent;
    public event Action LeftClickReleaseEvent;

    private Controls _controls;
    public Controls Controls => _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.UI.SetCallbacks(this);
        }

        _controls.Player.Enable();
        _controls.UI.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
        _controls.UI.Disable();
    }

    #region UIActions
    public void OnNavigate(InputAction.CallbackContext context)
    {

    }

    public void OnSubmit(InputAction.CallbackContext context)
    {

    }

    public void OnCancel(InputAction.CallbackContext context)
    {

    }

    public void OnPoint(InputAction.CallbackContext context)
    {

    }

    public void OnClick(InputAction.CallbackContext context)
    {

    }

    public void OnRightClick(InputAction.CallbackContext context)
    {

    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {

    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {

    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {

    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {

    }

#endregion 

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        Debug.Log("터치시작");
        if (context.performed)
        {
            LeftClickEvent?.Invoke();
        }
        else if(context.canceled)
        {
            Debug.Log("터치종료");
            LeftClickReleaseEvent?.Invoke();
        }
    }
}
