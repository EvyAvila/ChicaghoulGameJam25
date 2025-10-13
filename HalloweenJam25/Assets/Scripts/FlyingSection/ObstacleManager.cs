using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Obstacle Placement")]
    [SerializeField] private float spaceBetween;
    [SerializeField] private int spawnAmount = 1;
    [SerializeField] private GameObject obstacleSpawnerPrefab;

    private void Start()
    {
        Vector3 nextSpawnPoint = transform.position;

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject g = Instantiate(obstacleSpawnerPrefab, nextSpawnPoint, transform.rotation);
            g.transform.SetParent(transform);
            nextSpawnPoint.z += spaceBetween;
        }
    }
}
