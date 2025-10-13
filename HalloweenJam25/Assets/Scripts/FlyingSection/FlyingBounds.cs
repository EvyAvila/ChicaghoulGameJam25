using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBounds : MonoBehaviour
{

    [Header("Bounds Properties")]
    [SerializeField] private float Width;
    [SerializeField] private float Height;

    [Header("Corners")]
    private Vector3 BL;
    private Vector3 TL;
    private Vector3 TR;
    private Vector3 BR;

    [SerializeField] private bool DrawBounds;

    private void Start()
    {
        Vector3 local = transform.position;
        BL = new Vector3(local.x - Width, local.y - Height, local.z);
        TL = new Vector3(local.x - Width, local.y + Height, local.z);
        BR = new Vector3(local.x + Width, local.y - Height, local.z);
        TR = new Vector3(local.x + Width, local.y + Height, local.z);
    }

    private void Update()
    {
        if (DrawBounds)
            UpdateBoundsVisual();
    }
    private void UpdateBoundsVisual()
    {
        Vector3 local = transform.position;
        BL = new Vector3(local.x - Width, local.y - Height, local.z);
        TL = new Vector3(local.x - Width, local.y + Height, local.z);
        BR = new Vector3(local.x + Width, local.y - Height, local.z);
        TR = new Vector3(local.x + Width, local.y + Height, local.z);
    }

    public float GetWidth() { return Width; }
    public float GetHeight() { return Height; }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (DrawBounds)
        {
            Gizmos.DrawLine(BL, TL);
            Gizmos.DrawLine(TL, TR);
            Gizmos.DrawLine(TR, BR);
            Gizmos.DrawLine(BR, BL);
        }

    }
}
