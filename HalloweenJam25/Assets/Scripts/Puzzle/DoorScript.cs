using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorOpening;
    private Vector3 targetPos;

    public event Action OnDoorClose;
    public event Action OnDoorOpen;
    private void Start()
    {
        targetPos = transform.position;
    }
    public void Open()
    {
        if (doorOpening)
            return;

        targetPos = transform.localPosition + new Vector3(0, 7, 0);
        doorOpening = true;
        OnDoorOpen?.Invoke();
    }

    public void Close()
    {
        if (doorOpening)
            return;

        targetPos = transform.localPosition - new Vector3(0, 7, 0);
        doorOpening = true;
        OnDoorClose?.Invoke();
    }

    private void Update()
    {
        if (doorOpening)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 5 * Time.deltaTime);

            if (Mathf.Approximately(transform.position.y, targetPos.y)) 
                doorOpening = false;
        }
    }
}
