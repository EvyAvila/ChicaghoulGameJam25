using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction {NORTH, EAST, SOUTH, WEST}
public class StatueObject : StationaryItem
{
    /// <summary>
    /// Rotate speed of object
    /// </summary>
    [SerializeField] private float rotateSpeed = 70;

    /// <summary>
    /// Rotation of object
    /// </summary>
    public Direction FacingDirection { get { return facingDirection; } }
    private Direction facingDirection;

    //Rotation
    [SerializeField] private Transform parentTransform;
    private Quaternion nextDirection;
    private bool rotating;

    //Public Events
    public event Action OnStatueRotated;
    protected override void Start()
    {
        if (transform.forward == parentTransform.forward)
        {
            facingDirection = Direction.NORTH;
        }
        else if (transform.forward == parentTransform.right)
        {
            facingDirection = Direction.EAST;
        }
        else if (transform.forward == -parentTransform.right)
        {
            facingDirection = Direction.WEST;
        }
        else if (transform.forward == -parentTransform.forward)
        {
            facingDirection = Direction.SOUTH;
        }
    }
    public override void Interact()
    {
        if (rotating)
            return;

        SetRotate(90);
    }

    public override void SecondaryInteract()
    {
        if (rotating)
            return;

        SetRotate(-90);
    }

    private void SetRotate(float angle)
    {
        Vector3 next = transform.eulerAngles;
        next.y += angle;
        nextDirection = Quaternion.Euler(next);
        rotating = true;
    }
    protected override void Update()
    {
        if (rotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, nextDirection, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, nextDirection) < 1)
            {
                transform.rotation = nextDirection;

                rotating = false;
                float dot = Vector3.Dot(parentTransform.forward, transform.forward);

                if (dot > 0.9f)
                {
                    facingDirection = Direction.NORTH;
                }
                else if (dot < -0.9f)
                {
                    facingDirection = Direction.SOUTH;
                }
                else if (dot > -0.3 && dot < 0.3)
                {
                    if (transform.forward.x > 0)
                    {
                        facingDirection = Direction.EAST;
                    }
                    else
                    {
                        facingDirection = Direction.WEST;
                    }
                }

                OnStatueRotated?.Invoke();
            }
        }
    }
}
