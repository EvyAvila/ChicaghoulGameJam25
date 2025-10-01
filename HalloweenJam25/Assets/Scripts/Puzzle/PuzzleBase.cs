using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PuzzleBase : MonoBehaviour
{
    public UnityEvent OnPuzzleSolved;

    protected Predicate<PuzzleCondition> solveCondition;
    public virtual void SolvePuzzle()
    {
        OnPuzzleSolved?.Invoke();
    }
}
