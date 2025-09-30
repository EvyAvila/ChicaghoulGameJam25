using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The physical player body object
    /// </summary>
    [SerializeField] private GameObject playerBody;

    /// <summary>
    /// Transform raycast ground check
    /// </summary>
    /// 
    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundMask;
    private bool isgrounded;

    /// <summary>
    /// Attached rigidbody for movement
    /// </summary>
    private Rigidbody rb;
    [Header("Movement")]
    [SerializeField] private float acceleration;
    [SerializeField] private float deAccel;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float drag;


    /// <summary>
    /// Direction Vector
    /// </summary>
    private Vector3 movementVector;

    /// <summary>
    /// Vector used to apply movement
    /// </summary>
    private Vector3 finalMoveVector;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetMovementVector(Vector2 move)
    {
        movementVector.x = move.x;
        movementVector.z = move.y;
    }

    private void Movement()
    {
        Vector3 dir = (playerBody.transform.forward * movementVector.z) +
            (playerBody.transform.right * movementVector.x);

        dir.Normalize();

        Vector3 targetVelocity = dir * maxSpeed;

        Vector3 delta = targetVelocity - rb.velocity;

        if (movementVector.x != 0 || movementVector.z != 0)
        {
            rb.AddForce(delta * acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(delta * deAccel * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerBody.transform.position - Vector3.down, groundCheckRadius);
    }
}
