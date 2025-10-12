using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCondition : PuzzleCondition
{
    [SerializeField] private GameObject StatueA;
    [SerializeField] private Direction ADirection;
    [SerializeField] private GameObject StatueB;
    [SerializeField] private Direction BDirection;
    [SerializeField] private GameObject StatueC;
    [SerializeField] private Direction CDirection;
    [SerializeField] private GameObject StatueD;
    [SerializeField] private Direction DDirection;
    public override bool isCorrect()
    {
        if (!VerifyStatueDirection(StatueA, ADirection))
            return false;

        if (!VerifyStatueDirection(StatueB, BDirection))
            return false;

        if (!VerifyStatueDirection(StatueC, CDirection))
            return false;

        if (!VerifyStatueDirection (StatueD, DDirection))
            return false;

        Debug.Log("Condition Met");
        return true;
    }

    //Helper for directions
    private bool VerifyStatueDirection(GameObject s, Direction direction)
    {
        return s.GetComponent<StatueObject>().FacingDirection == direction;
    }
}
