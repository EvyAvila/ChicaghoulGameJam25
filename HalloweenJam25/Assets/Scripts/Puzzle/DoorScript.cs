using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorOpening;
    private Vector3 targetPos;

    private void Start()
    {
        targetPos = transform.position;
    }
    public void Open()
    {
        if (doorOpening)
            return;

        targetPos.y += 5;
        doorOpening = true;
    }

    public void Close()
    {
        if (doorOpening)
            return;

        targetPos.y -= 5;
        doorOpening = true;
    }

    private void Update()
    {
        if (doorOpening)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 5 * Time.deltaTime);

            if (Mathf.Approximately(transform.position.y, targetPos.y)) 
                doorOpening = false;
        }
    }
}
