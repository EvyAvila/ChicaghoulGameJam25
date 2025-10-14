using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event Action OnObstacleCollision;
    private void OnTriggerEnter(Collider other)
    {
        if (BloodTracker.Instance != null)
            BloodTracker.TakeDamage();

        OnObstacleCollision?.Invoke();
    }
}
