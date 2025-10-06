using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class Rotator : MonoBehaviour
{

    public static event Action<float> rotated;

    /// <summary>
    /// When the object holding the potions fininshes rotating, invoke the method
    /// for the cauldron to check which potion is above them.
    /// </summary>
    /// <param name="degrees"></param>
    public void RotateObject(float degrees)
    {
        Vector3 v = new Vector3(0f, degrees, 0f);
        transform.DORotate(v, 0.2f, RotateMode.WorldAxisAdd)
            .OnComplete(() => 
            {
                rotated?.Invoke(degrees);
            }); 
    
    }
}