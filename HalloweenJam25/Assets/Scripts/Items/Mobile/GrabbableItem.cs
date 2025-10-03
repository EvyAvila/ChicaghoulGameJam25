using System;
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
    protected Rigidbody rb;

    /// <summary>
    /// Transform that the object will follow
    /// </summary>
    [SerializeField] protected Transform hookPoint;

    /// <summary>
    /// Speed of item to follow its hookPoint
    /// </summary>
    [SerializeField] protected float hookMoveSpeed = 5.0f;

    /// <summary>
    /// Drag applied to object when grabbed
    /// </summary>
    [SerializeField] protected float grabDrag = 25;

    /// <summary>
    /// DIstance at which grab disengages
    /// </summary>
    [SerializeField] private float maxBreakDistance = 3.0f;

    /// <summary>
    /// Toggle for keeping item aligned with player while carrying
    /// </summary>
    protected bool alignWithPlayer;

    /// <summary>
    /// Side to align with player grab
    /// </summary>
    [SerializeField] protected Direction alignSide;

    //Velocity
    private Vector3 exitVelocity;
    private Vector3 prevPosition;

    //Exposed Events
    public event Action OnForgetItem; //--Used to dereference in player class, DOESN'T call StopInteract()

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HookToPoint()
    {
        Vector3 toHook = hookPoint.position - transform.position;

        Debug.DrawRay(transform.position, toHook, Color.red);
        rb.AddForce(toHook * hookMoveSpeed, ForceMode.Force);
        
        //Break grab if too far
        if (toHook.magnitude > maxBreakDistance)
        {
            StopInteraction();
            return;
        }

        if (alignWithPlayer)
        {
            Vector3 side = hookPoint.forward;
            
            switch (alignSide)
            {
                case Direction.NORTH:
                    side = hookPoint.forward;
                    break;
                case Direction.EAST:
                    side = hookPoint.right;
                    break;
                case Direction.SOUTH:
                    side = -hookPoint.forward;
                    break;
                case Direction.WEST:
                    side = -hookPoint.right;
                    break;
                default:
                    side = hookPoint.forward;
                    break;
            }
            Quaternion targetRotation = Quaternion.LookRotation(side, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 400 * Time.fixedDeltaTime);
        }

        exitVelocity = (transform.position - prevPosition) / Time.deltaTime;
        prevPosition = transform.position;
    }

    protected override void FixedUpdate()
    {
        if (hookPoint == null)
            return;
    
        HookToPoint();
    }

    /// <summary>
    /// Used for dereferencing the item from within the Player.
    /// Does not call StopInteraction()
    /// </summary>
    protected void ForgetItem()
    {
        OnForgetItem?.Invoke();
    }

    //Override----------------
    public override void Interact(Transform hook)
    {
        hookPoint = hook;

        if (rb.isKinematic)
            rb.isKinematic = false;
        
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
