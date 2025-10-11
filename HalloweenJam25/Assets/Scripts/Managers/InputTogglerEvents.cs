using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton event messanger for inputs
/// </summary>
public class InputTogglerEvents : MonoBehaviour
{
    public static InputTogglerEvents Instance {  get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public static event Action OnDisablePlayerInputs;
    public static event Action OnReEnablePlayerInputs;

    public static void DisablePlayerInputs()
    {
        OnDisablePlayerInputs?.Invoke();
    }

    public static void EnablePlayerInputs()
    {
        OnReEnablePlayerInputs?.Invoke();
    }
}
