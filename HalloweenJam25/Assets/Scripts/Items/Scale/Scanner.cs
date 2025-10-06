using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scanner : MonoBehaviour
{

    public static event Action<ItemIdentifier> itemObj;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ItemIdentifier>())
        {
            itemObj?.Invoke(other.gameObject.GetComponent<ItemIdentifier>());
        }
        
        
    }
}
