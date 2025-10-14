using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class that holds Puzzle prefabs
/// </summary>
public class PuzzlePool : MonoBehaviour
{
    [SerializeField] private List<GameObject> PuzzlesPrefabs;
    public static PuzzlePool Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public GameObject GetPuzzle()
    {
        int rand = UnityEngine.Random.Range(0, PuzzlesPrefabs.Count);

        GameObject o = PuzzlesPrefabs[rand];

        //PuzzlesPrefabs.RemoveAt(rand);

        return o;
    }    
}
