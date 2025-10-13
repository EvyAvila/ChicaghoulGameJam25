using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrack : MonoBehaviour
{
    [SerializeField] private float trackSpeed;

    private void Update()
    {
        transform.localPosition += Vector3.back * trackSpeed * Time.deltaTime;
    }
}
