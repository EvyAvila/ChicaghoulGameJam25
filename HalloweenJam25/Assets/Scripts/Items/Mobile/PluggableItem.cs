using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PluggableItem : GrabbableItem, IPluggable
{
    /// <summary>
    /// Describes the hooked type
    /// </summary>
    protected bool socketGrab;

    /// <summary>
    /// If object is in process of connecting
    /// </summary>
    private bool connecting;

    /// <summary>
    /// Speed of item plugging in
    /// </summary>
    private const float slotInSpeed = 5.0f;

    //Public Events Actions
    public event Action OnItemDisconnect;
    
    protected override void Start()
    {
        base.Start();
        alignWithPlayer = true;
    }

    protected override void FixedUpdate()
    {
        if (connecting)
        {
            transform.position = Vector3.Lerp(transform.position, hookPoint.position, slotInSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, hookPoint.rotation, 50*Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, hookPoint.rotation) < 1)
            {
                transform.rotation = hookPoint.rotation;
            }

            if (Vector3.Distance(transform.position, hookPoint.position) < 1)
            {
                transform.position = hookPoint.position;
            }

            if (transform.position == hookPoint.position && 
                transform.rotation == hookPoint.rotation)
            {
                connecting = false;
            }
        }
        else
        {
            base.FixedUpdate();
        }
    }

    //Override-----------
    public override void Interact(Transform t)
    {
        base.Interact(t);
        
        if (socketGrab)
            rb.isKinematic = true;
    }

    //Interface----------
    public void ConnectToPoint(Transform t)
    {
        socketGrab = true;
        Interact(t);
    }
    public void Disconnect()
    {
        socketGrab = false;
        rb.isKinematic = false;

        OnItemDisconnect?.Invoke();
    }
}
