using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInitializer : MonoBehaviour
{
    [Header("Puzzle Components")]
    [SerializeField] private PuzzleSubmitter submitter;
    [SerializeField] private Transform puzzlePoint;
    private GameObject puzzleObject;
    private PuzzleBase puzzleInstance;
    private void Start()
    {
        if (PuzzlePool.Instance != null)
            puzzleObject = PuzzlePool.Instance.GetPuzzle();

        if (puzzleObject != null)
        {
            Debug.Log($"Instantiating {puzzleObject.name}");
            GameObject g = Instantiate(puzzleObject, puzzlePoint);
            puzzleInstance = g.GetComponent<PuzzleBase>();
        
            if (submitter != null)
            {
                submitter.SetTrackedPuzzle(puzzleInstance);
            }
        }
        else
        {
            Debug.Log("Puzzle was null");
        }
    }
}
