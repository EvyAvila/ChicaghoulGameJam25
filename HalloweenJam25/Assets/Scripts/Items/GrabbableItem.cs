using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrabbableItem : InteractableObject
{
    /// <summary>
    /// The items rigidbody componenet
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Transform that the object will follow
    /// </summary>
    [SerializeField] private Transform hookPoint;

    /// <summary>
    /// Speed of item to follow its hookPoint
    /// </summary>
    [SerializeField] private float hookMoveSpeed = 5.0f;

    /// <summary>
    /// Drag applied to object when grabbed
    /// </summary>
    [SerializeField] private float grabDrag = 25;

    /// <summary>
    /// DIstance at which grab disengages
    /// </summary>
    [SerializeField] private float maxBreakDistance = 3.0f;

    private Vector3 exitVelocity;
    private Vector3 prevPosition;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HookToPoint()
    {
        if (hookPoint == null)
            return;

        Vector3 toHook = hookPoint.position - transform.position;
        if (toHook.magnitude > maxBreakDistance)
        {
            StopInteraction();
        }

        Debug.DrawRay(transform.position, toHook, Color.red);
        rb.AddForce(toHook * hookMoveSpeed, ForceMode.Force);

        exitVelocity = (transform.position - prevPosition) / Time.deltaTime;
        prevPosition = transform.position;
    }
    protected override void FixedUpdate()
    {
        HookToPoint();
    }
    
    //Override----------------
    public override void Interact(Transform hook)
    {
        hookPoint = hook;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.drag = grabDrag;
    }
    public override void StopInteraction()
    {
        if (hookPoint == null)
            return;
        
        hookPoint = null;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.None;
        rb.drag = 1.0f;
        rb.AddForce(exitVelocity * rb.mass, ForceMode.VelocityChange);
    }

}
