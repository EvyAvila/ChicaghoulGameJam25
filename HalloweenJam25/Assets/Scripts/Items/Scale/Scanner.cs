using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scanner : MonoBehaviour
{

    public static event Action<ItemIdentifier> itemObj;
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ItemIdentifier>())
        {
            //Debug.Log(other.GetComponent<ItemIdentifier>().itemName);

            itemObj?.Invoke(other.gameObject.GetComponent<ItemIdentifier>());
        }
        
        
    }
}
