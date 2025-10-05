using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PotionPuzzle : PuzzleBase
{
    /// <summary>
    /// Get the custom condition
    /// </summary>
    private PotionCondition condition;

    
    void Start()
    {
        condition = GetComponent<PotionCondition>();
        
        solveCondition = s => s.isCorrect();

        //Randomize the order of conditions required

        Cauldron.selected += OnUpdateDetected;
        
    }

    private void OnUpdateDetected(PotionColor color)
    {
        //condition.enteredColors.Add(color);
        condition.CanEnterAnswer(color);
        if (solveCondition(condition))
        {
            SolvePuzzle();
        }
    }

    private void OnDisable()
    {
        Cauldron.selected -= OnUpdateDetected;
    
        
    }
}
