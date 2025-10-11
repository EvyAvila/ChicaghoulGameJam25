using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomEnterTrigger : MonoBehaviour
{
    /// <summary>
    /// Event that will start subscribed PuzzleTimer
    /// </summary>
    [HideInInspector]
    public UnityEvent triggeredEvent;
 
    public bool triggerActivated = false;
    private void OnTriggerEnter(Collider other)
    {
        if (triggerActivated)
            return;

        triggerActivated = true;
        triggeredEvent?.Invoke();
    }
}
