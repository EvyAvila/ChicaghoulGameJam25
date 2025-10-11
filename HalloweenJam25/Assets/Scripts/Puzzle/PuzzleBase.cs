using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Abstract template for puzzles
/// </summary>
public abstract class PuzzleBase : MonoBehaviour
{
    protected bool puzzleLocked;

    /// <summary>
    /// Condition to solve puzzle
    /// </summary>
    protected Predicate<PuzzleCondition> solveCondition;
    
    /// <summary>
    /// External object action Event
    /// </summary>
    public UnityEvent OnPuzzleSolved;

    /// <summary>
    /// Activate solved logic
    /// </summary>
    public virtual void SolvePuzzle()
    {
        OnPuzzleSolved?.Invoke();
    }
}
