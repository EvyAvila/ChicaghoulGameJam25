using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyboardNote 
{
    public bool HeldDown;
    public bool PreviouslyHeld;
    public void Reset()
    {
        HeldDown = false;
        PreviouslyHeld = false;
    }
}
