using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base puzzle condition class
/// </summary>
public abstract class PuzzleCondition : MonoBehaviour
{
    /// <summary>
    /// Abstract method to check if puzzle condition is met
    /// </summary>
    /// <returns>True/False</returns>
    public abstract bool isCorrect();
}
