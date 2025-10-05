using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PourButtonObject : InteractableObject //, IButtonObjects
{
    [SerializeField] private ButtonOptions buttonOptions;

    [SerializeField] private Cauldron cauld;

    public bool isInteractable { get; set; }

    public static event Action<float> onInteractionChanged;

    protected override void Start()
    {
        isInteractable = true;
    }

    public override void Interact()
    {
        if(isInteractable && !cauld.isSolved)
        {
            cauld.GetPotion();
            onInteractionChanged?.Invoke(cauld.wait);
        }
    }
}
