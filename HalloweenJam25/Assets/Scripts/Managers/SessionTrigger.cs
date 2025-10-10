using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is for the very beginning of a session. Initiates the timer.
/// </summary>
public class SessionTrigger : MonoBehaviour
{
    public static event Action OnSessionTriggerEnter;
    private bool activated;
    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        activated = true;
        OnSessionTriggerEnter?.Invoke();
    }
}
