using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHub : MonoBehaviour
{
    /// <summary>
    /// Input Component
    /// </summary>
    private RawInputReader inputReader;

    /// <summary>
    /// Direction for movement
    /// </summary>
    private Vector2 movementVector;

    /// <summary>
    /// Camera aiming component
    /// </summary>
    private CameraAimer cameraAimer;

    // Start is called before the first frame update
    void Start()
    {
        inputReader = GetComponent<RawInputReader>();
        cameraAimer = GetComponent<CameraAimer>();

        inputReader.OnDirectionPerfomed += OnMovementPerformed;
        inputReader.OnDirectionStopped += OnMovementStopped;
    }

    private void Update()
    {
        cameraAimer.SetMouseAim(inputReader.AimDelta);
    }

    private void OnMovementPerformed(object sender, Vector2 e)
    {
        movementVector = e;
    }
    private void OnMovementStopped()
    {
        movementVector = Vector2.zero;
    }

    private void OnDisable()
    {
        inputReader.OnDirectionPerfomed -= OnMovementPerformed;
        inputReader.OnDirectionStopped -= OnMovementStopped;
    }
}
