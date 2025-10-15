using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingButtonObjects : InteractableObject //, IButtonObjects
{
    [SerializeField] private ButtonOptions buttonOptions;

    [SerializeField] private Rotator rot;

    [SerializeField] private float degrees;

    [SerializeField] private Cauldron cauldron;

    private float buttonTimer = 0.4f;
    private float currTimer;
    public bool isInteractable { get; set; }

    protected override void Start()
    {
        isInteractable = true;
    }

    protected override void Update()
    {
        if (isInteractable)
            return;

        currTimer += Time.deltaTime;

        if (currTimer > buttonTimer)
        {
            currTimer = buttonTimer;
            isInteractable = true;
        }
    }

    public override void Interact()
    {
        if (cauldron.isPouring)
            return;

        if (!isInteractable)
            return;

        isInteractable = false;
        currTimer = 0.0f;

        switch (buttonOptions)
        {
            case ButtonOptions.TurnLeft:
                //if (isInteractable)

                rot.RotateObject(degrees);
                break;
            case ButtonOptions.TurnRight:
                //if (isInteractable)
                  
                rot.RotateObject(-degrees);
                break;
        }

    }
}
