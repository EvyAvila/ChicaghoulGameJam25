using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ButtonOptions { TurnLeft, TurnRight, Pour, None}

public class ButtonObjects : InteractableObject
{
    /*
    [SerializeField] private ButtonOptions buttonOptions;

    private Rotator rot;
    private Cauldron cauld;

    [SerializeField] private float degrees;

    [SerializeField] private GameObject Script;

    public bool canInteract { get; set; }

    public static event Action interact;

    protected override void Start()
    {
        switch (buttonOptions)
        {
            case ButtonOptions.TurnLeft:
            case ButtonOptions.TurnRight:
                rot = Script.GetComponent<Rotator>();
                break;
            case ButtonOptions.Pour:
                cauld= Script.GetComponent<Cauldron>();
                break;
            default:
                break;
        }
    }

    public override void Interact()
    {
        if(canInteract)
        {
            switch (buttonOptions)
            {
                case ButtonOptions.TurnLeft:
                    rot.RotateObject(degrees);
                    break;
                case ButtonOptions.TurnRight:
                    rot.RotateObject(-degrees);
                    break;
                case ButtonOptions.Pour:
                    cauld.GetPotion();
                    canInteract = false;
                    interact?.Invoke();
                    break;
                default:
                    Debug.Log("none");
                    break;
            }
        }
        
    }*/
}

interface IButtonObjects 
{
    public bool isInteractable { get; set; }
}