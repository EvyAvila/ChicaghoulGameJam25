using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInitializer : MonoBehaviour
{
    [Header("Puzzle Components")]
    [SerializeField] private PuzzleSubmitter submitter;
    [SerializeField] private Transform puzzlePoint;
    [SerializeField] private GameObject puzzleObject;
    [SerializeField] private bool useInspectorPuzzle;
    
    private PuzzleBase puzzleInstance;
    private void Start()
    {
        if (useInspectorPuzzle && puzzleObject != null)
        {
            GameObject g = Instantiate(puzzleObject, puzzlePoint);
            puzzleInstance = g.GetComponent<PuzzleBase>();

            if (puzzleInstance == null)
                puzzleInstance = g.GetComponentInChildren<PuzzleBase>();

            submitter.SetTrackedPuzzle(puzzleInstance);

            return;
        }

        if (PuzzlePool.Instance != null)
            puzzleObject = PuzzlePool.Instance.GetPuzzle();

        if (puzzleObject != null)
        {
            Debug.Log($"Instantiating {puzzleObject.name}");
            GameObject g = Instantiate(puzzleObject, puzzlePoint);
            puzzleInstance = g.GetComponent<PuzzleBase>();
            submitter.SetTrackedPuzzle(puzzleInstance);
        }
        else
        {
            Debug.Log("Puzzle was null");
        }
    }
}
