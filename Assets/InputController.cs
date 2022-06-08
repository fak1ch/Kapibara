using System;
using TouchNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class InputController : MonoBehaviour
{
    private Camera _mainCamera;
    private TouchControls _touchControls;

    #region Events
    public delegate void StartTouch(Vector3 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector3 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private void Awake()
    {
        _touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        _touchControls.Enable();
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();

        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += StartPrimaryTouch;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += EndPrimaryTouch;
    }

    private void OnDisable()
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= StartPrimaryTouch;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= EndPrimaryTouch;

        _touchControls.Disable();
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void StartPrimaryTouch(Finger finger)
    {
        if (OnStartTouch != null)
            OnStartTouch(Utils.ScreenToWorld(_mainCamera, finger.screenPosition), Time.time);
    }

    private void EndPrimaryTouch(Finger finger)
    {
        if (OnEndTouch != null)
            OnEndTouch(Utils.ScreenToWorld(_mainCamera, finger.screenPosition), Time.time);
    }

    public Vector3 GetTouchPosition()
    {
        Vector3 position = Utils.ScreenToWorld(_mainCamera, _touchControls.Touch.PrimaryPosition.ReadValue<Vector2>());
        return position;
    }
}
