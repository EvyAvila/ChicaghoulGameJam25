using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RawInputReader : MonoBehaviour
{
    private PlayerInputs _inputs;

    //Exposed Events
    public EventHandler<Vector2> OnDirectionPerfomed;
    public event Action OnDirectionStopped;
    public event Action OnInteractHeld;
    public event Action OnInteractStop;
    public event Action OnInteractSecondary;
    public event Action OnRotate;
    public event Action OnRotateStopped;

    public Vector2 AimDelta { get; private set; }

    private void OnEnable()
    {
        if (_inputs == null)
            _inputs = new PlayerInputs();

        _inputs.Enable();
        _inputs.GroundMap.Directions.performed += OnDirectionPressed;
        _inputs.GroundMap.Directions.canceled += OnDirectionsStopped;
        _inputs.GroundMap.Interact.performed += OnInteractPerformed;
        _inputs.GroundMap.SecondaryInteract.performed += OnSecondaryInteractPerformed;
        _inputs.GroundMap.Interact.canceled += OnInteractStopped;
        _inputs.GroundMap.ToggleRotate.started+= OnToggleRotate;
    }

    private void OnSecondaryInteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractSecondary?.Invoke();
    }

    private void OnToggleRotate(InputAction.CallbackContext obj)
    {
        OnRotate?.Invoke();
    }

    private void OnInteractStopped(InputAction.CallbackContext obj)
    {
        OnInteractStop?.Invoke();
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractHeld?.Invoke();
    }

    private void Update()
    {
        AimDelta = _inputs.GroundMap.Aim.ReadValue<Vector2>();
    }
    private void OnDirectionsStopped(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDirectionStopped?.Invoke();
    }

    private void OnDirectionPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDirectionPerfomed?.Invoke(this, obj.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.GroundMap.Directions.performed -= OnDirectionPressed;
        _inputs.GroundMap.Directions.canceled -= OnDirectionsStopped;
        _inputs.GroundMap.Interact.performed -= OnInteractPerformed;
        _inputs.GroundMap.SecondaryInteract.performed -= OnSecondaryInteractPerformed;
        _inputs.GroundMap.Interact.canceled -= OnInteractStopped;
        _inputs.GroundMap.ToggleRotate.performed -= OnToggleRotate;
    }
}
