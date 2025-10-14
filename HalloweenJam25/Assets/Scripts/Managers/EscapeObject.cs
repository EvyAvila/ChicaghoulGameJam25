using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeObject : InteractableObject
{
    public static event Action<bool> OnPlayerEscaped;

    /// <summary>
    /// Whether to call event when OnCollision
    /// </summary>
    [SerializeField] private bool usingCollider;
    private bool activated;

    //Overrides---------------------
    public override void Interact()
    {
        if (usingCollider)
            return;
        
        if (activated)
            return;

        activated = true;
        OnPlayerEscaped?.Invoke(usingCollider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!usingCollider)
            return;

        if (activated)
            return;

        activated = true;
        OnPlayerEscaped?.Invoke(usingCollider);
    }
}
