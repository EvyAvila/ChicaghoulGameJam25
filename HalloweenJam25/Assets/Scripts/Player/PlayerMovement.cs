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
    [Header("Ground")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundMask;
    private bool isgrounded;

    [Header("Gravity")]
    [SerializeField] private float gravityAccel = 8;
    [SerializeField] private float gravityMax = 10;
    private float gravity;

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
    private Vector3 horizontalVector;

    /// <summary>
    /// Vector used to apply movement
    /// </summary>
    private Vector3 verticalVector;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetMovementVector(Vector2 move)
    {
        horizontalVector.x = move.x;
        horizontalVector.z = move.y;
    }
    private void GroundCheck()
    {
        if (Physics.SphereCast(playerBody.transform.position, groundCheckRadius, Vector3.down, out RaycastHit hit, 1, groundMask.value))
        {            
            isgrounded = true;

            gravity = 0.0f;
            Vector3 targetPos = rb.position;
            targetPos.y = hit.point.y;

            rb.position = Vector3.Lerp(rb.position, targetPos, Time.fixedDeltaTime * 5);
        }
        else
        {
            isgrounded = false;

            gravity -= gravityAccel * Time.deltaTime;
            if (gravity > gravityMax)
                gravity = gravityMax;

            verticalVector.y = gravity;
            rb.AddForce(verticalVector, ForceMode.VelocityChange);
        }
    }
    private void Movement()
    {
        Vector3 dir = (playerBody.transform.forward * horizontalVector.z) +
            (playerBody.transform.right * horizontalVector.x);

        dir.Normalize();

        Vector3 targetVelocity = dir * maxSpeed;

        Vector3 delta = targetVelocity - rb.velocity;

        if (horizontalVector.x != 0 || horizontalVector.z != 0)
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
        GroundCheck();
        Movement();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerBody.transform.position + Vector3.down, groundCheckRadius);
    }
}
