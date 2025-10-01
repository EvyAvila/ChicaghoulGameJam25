using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : PuzzleBase
{
    [SerializeField] private List<StatueObject> statues;

    private StatueCondition condition;
    private void Start()
    {
        condition = GetComponent<StatueCondition>();

        solveCondition = s => s.isCorrect();

        foreach (StatueObject statue in statues)
        {
            statue.OnStatueRotated += OnStatueChange;
        }
    }

    private void OnStatueChange()
    {
        if (solveCondition(condition))
            SolvePuzzle();
    }

    private void OnDisable()
    {
        foreach (StatueObject statue in statues)
        {
            statue.OnStatueRotated -= OnStatueChange;
        }
    }
}
