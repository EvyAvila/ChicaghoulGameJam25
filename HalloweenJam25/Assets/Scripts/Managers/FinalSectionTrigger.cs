using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sends event when player reaches the final section
/// </summary>
public class FinalSectionTrigger : MonoBehaviour
{
    public static event Action OnReachFinal;
    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        activated = true;
        OnReachFinal?.Invoke();
    }

}
