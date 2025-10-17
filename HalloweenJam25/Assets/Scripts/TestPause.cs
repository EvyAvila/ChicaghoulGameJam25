using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPause : MonoBehaviour
{
    private PlayerInputs _inputs;
   
    private bool isActive;

    private void Start()
    {
        UIManager.Instance.DisplayPauseMenu(isActive);
    }

    private void OnEnable()
    {
        if (_inputs == null)
            _inputs = new PlayerInputs();

        _inputs.Enable();

        isActive = false;

        _inputs.PauseMap.Pause.performed += OnPaused;
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.PauseMap.Pause.performed -= OnPaused;
    }

    private void OnPaused(InputAction.CallbackContext obj)
    {
        isActive = !isActive;

        if (isActive)
        {
            UIManager.Instance.DisplayPauseMenu(isActive);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UIManager.Instance.DisplayPauseMenu(isActive);
        }
    }
}