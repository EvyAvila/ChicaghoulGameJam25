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

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HookToPoint()
    {
        Vector3 toHook = hookPoint.position - transform.position;
        if (toHook.magnitude > maxBreakDistance)
        {
            StopInteraction();
        }

        Debug.DrawRay(transform.position, toHook, Color.red);
        rb.AddForce(toHook * hookMoveSpeed, ForceMode.Force);

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
