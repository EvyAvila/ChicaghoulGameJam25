using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeObject : InteractableObject
{
    public static event Action OnPlayerEscaped;

    /// <summary>
    /// Whether to call event when OnCollision
    /// </summary>
    private bool Collider;
    
    //Overrides---------------------
    public override void Interact()
    {
        if (Collider)
            return;

        OnPlayerEscaped?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Collider)
            return;

        OnPlayerEscaped?.Invoke();
    }
}
