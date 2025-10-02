using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSocket : MonoBehaviour
{
    private GameObject pluggedItem;
    
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
            i.ConnectToPoint(transform);
            other.gameObject.GetComponent<PluggableItem>().OnItemDisconnect += OnItemDisconnect;
            pluggedItem = other.gameObject;
        }

        Debug.Log("Item plugged - " + pluggedItem.name);
    }

    private void OnItemDisconnect()
    {
        pluggedItem.GetComponentInChildren<PluggableItem>().OnItemDisconnect -= OnItemDisconnect;
        pluggedItem = null;
    }
}
