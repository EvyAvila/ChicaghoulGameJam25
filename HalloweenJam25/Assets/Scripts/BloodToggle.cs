using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class BloodToggle : InteractableObject
{
    public static event Action<float> OnCollectBlood;
    public float bloodValue;

    public override void Interact()
    {
        OnCollectBlood?.Invoke(bloodValue);
    }

    
}
