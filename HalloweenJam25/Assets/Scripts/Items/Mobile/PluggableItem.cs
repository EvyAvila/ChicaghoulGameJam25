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
    /// If object is in process of inserting
    /// </summary>
    private bool inserting;

    /// <summary>
    /// Speed of item plugging in
    /// </summary>
    protected const float insertSpeed = 5.0f;

    /// <summary>
    /// Temporarily disables 'plugging' interaction 
    /// to ease instant inserting after unplugging
    /// </summary>
    protected bool allowInsert;
    protected float insertCooldown = 1.0f;
    protected float currInsertCooldown;

    //Public Events Actions
    public event Action OnItemDisconnect;

    protected override void Start()
    {
        base.Start();
        allowInsert = true;
        alignWithPlayer = true;
    }

    protected override void Update()
    {
        if (!allowInsert)
        {
            currInsertCooldown += Time.deltaTime;

            if (currInsertCooldown >= insertCooldown)
                allowInsert = true;
        }
    }
    protected override void FixedUpdate()
    {
        if (hookPoint == null)
            return;

        if (inserting)
        {
            transform.position = Vector3.Lerp(transform.position, hookPoint.position, insertSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, hookPoint.rotation, 400*Time.deltaTime);

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
                inserting = false;
            }
        }
        else
        {
            base.FixedUpdate();
        }
    }

    public override void Interact(Transform t)
    {
        base.Interact(t);

        if (socketGrab)
            Disconnect();
    }

    //Interface----------
    public bool ConnectToPoint(Transform t)
    {
        if (!allowInsert)
            return false;

        socketGrab = true;
        rb.isKinematic = true;
        inserting = true;
        hookPoint = t;
        ForgetItem();

        return true;
    }
    public void Disconnect()
    {
        allowInsert = false;
        currInsertCooldown = 0.0f;
        socketGrab = false;
        rb.isKinematic = false;

        OnItemDisconnect?.Invoke();
    }
}
