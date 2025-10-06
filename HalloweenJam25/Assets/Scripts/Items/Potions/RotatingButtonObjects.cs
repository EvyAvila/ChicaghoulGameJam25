using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingButtonObjects : InteractableObject //, IButtonObjects
{
    [SerializeField] private ButtonOptions buttonOptions;

    [SerializeField] private Rotator rot;

    [SerializeField] private float degrees;

    public bool isInteractable { get; set; }

    protected override void Start()
    {
        isInteractable = true;
    }


    public override void Interact()
    {
        switch (buttonOptions)
        {
            case ButtonOptions.TurnLeft:
                if (isInteractable)
                    rot.RotateObject(degrees);
                break;
            case ButtonOptions.TurnRight:
                if (isInteractable)
                    rot.RotateObject(-degrees);
                break;
        }
    }
}
