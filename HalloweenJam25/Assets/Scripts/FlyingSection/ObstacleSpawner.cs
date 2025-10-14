using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Points")]
    [SerializeField] private List<Transform> groundPoints;
    [SerializeField] private List<Transform> cielingPoints;

    [Header("Obstacles")]
    [SerializeField] private List<GameObject> groundObstacles;
    [SerializeField] private List<GameObject> cielingObstacles;

    private void Start()
    {

        for (int i = 0; i < 2; i++)
        {
            int obType = UnityEngine.Random.Range(0, 2);

            if (obType == 0)
            {
                SpawnType(ref groundPoints, ref groundObstacles);
            }
            else
            {
                SpawnType(ref cielingPoints, ref cielingObstacles);
            }
        }
    }

    private void SpawnType(ref List<Transform> points, ref List<GameObject> obstacles)
    {
        int index = UnityEngine.Random.Range(0, obstacles.Count);
        GameObject g = obstacles[index];

        index = UnityEngine.Random.Range(0, points.Count);
        Transform point = points[index];

        points.RemoveAt(index);

        Instantiate(g, point);
    }
}
