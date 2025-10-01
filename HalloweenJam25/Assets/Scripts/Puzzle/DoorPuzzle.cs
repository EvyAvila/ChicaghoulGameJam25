using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : PuzzleBase
{
    /// <summary>
    /// List of used interactables
    /// </summary>
    [SerializeField] private List<StatueObject> statues;

    /// <summary>
    /// Derived custom condition
    /// </summary>
    private StatueCondition condition;
    private void Start()
    {
        //Grab condition
        condition = GetComponent<StatueCondition>();
        
        //Initialize the Predicate (can't do that in abstract base)
        solveCondition = s => s.isCorrect();
        
        //Listen for statue rotates
        foreach (StatueObject statue in statues)
        {
            statue.OnStatueRotated += OnStatueChange;
        }
    }

    //Event Listener
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
