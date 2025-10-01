using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryItem : InteractableObject
{
    public override void Interact()
    {
        Debug.Log("I'm stationary");
    }
}
