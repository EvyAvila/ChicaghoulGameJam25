using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCondition : PuzzleCondition
{
    [SerializeField] private GameObject StatueA;
    [SerializeField] private GameObject StatueB;
    [SerializeField] private GameObject StatueC;
    [SerializeField] private GameObject StatueD;
    public override bool isCorrect()
    {
        if (!VerifyStatueDirection(StatueA, Direction.NORTH))
            return false;

        if (!VerifyStatueDirection(StatueB, Direction.WEST))
            return false;

        if (!VerifyStatueDirection(StatueC, Direction.NORTH))
            return false;

        if (!VerifyStatueDirection (StatueD, Direction.EAST))
            return false;

        Debug.Log("Condition Met");
        return true;
    }

    private bool VerifyStatueDirection(GameObject s, Direction direction)
    {
        return s.GetComponent<StatueObject>().FacingDirection == direction;
    }
}
