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
    /// Camera aiming component
    /// </summary>
    private CameraAimer cameraAimer;

    /// <summary>
    /// Movement component
    /// </summary>
    private PlayerMovement playerMovement;

    /// <summary>
    /// Player to object interact component
    /// </summary>
    private WorldInteracter worldInteracter;

    // Start is called before the first frame update
    void Start()
    {
        inputReader = GetComponent<RawInputReader>();
        cameraAimer = GetComponent<CameraAimer>();
        playerMovement = GetComponent<PlayerMovement>();
        worldInteracter = GetComponent<WorldInteracter>();

        inputReader.OnDirectionPerfomed += OnMovementPerformed;
        inputReader.OnDirectionStopped += OnMovementStopped;
        inputReader.OnInteractStop += OnInteractStopped;
        inputReader.OnInteractHeld += OnInteractHeld;
    }

    private void OnInteractHeld()
    {
        worldInteracter.CheckForInteract();
    }
    private void OnInteractStopped()
    {
        worldInteracter.StopInteracting();
    }
    private void Update()
    {
        cameraAimer.SetMouseAim(inputReader.AimDelta);
    }

    private void OnMovementPerformed(object sender, Vector2 e)
    {
        playerMovement.SetMovementVector(e);
    }
    private void OnMovementStopped()
    {
        playerMovement.SetMovementVector(Vector2.zero);
    }

    private void OnDisable()
    {
        inputReader.OnDirectionPerfomed -= OnMovementPerformed;
        inputReader.OnDirectionStopped -= OnMovementStopped;
        inputReader.OnInteractStop -= OnInteractStopped;
        inputReader.OnInteractHeld -= OnInteractHeld;
    }
}
