using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSocket : MonoBehaviour
{
    private GameObject pluggedItem;

    //Public Events
    public event Action OnItemAdded;
    public GameObject? GetPluggedItem()
    {
        return pluggedItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pluggedItem != null)
            return;

        if (other.gameObject.TryGetComponent<IPluggable>(out IPluggable i))
        {
            bool connectSuccess = i.ConnectToPoint(transform);
            
            //Disregard if item connect is disabled
            if (!connectSuccess)
                return;

            other.gameObject.GetComponent<PluggableItem>().OnItemDisconnect += OnItemDisconnect;
            pluggedItem = other.gameObject;

            OnItemAdded?.Invoke();
        }
    }

    private void OnItemDisconnect()
    {
        pluggedItem.GetComponentInChildren<PluggableItem>().OnItemDisconnect -= OnItemDisconnect;
        pluggedItem = null;
    }
}
