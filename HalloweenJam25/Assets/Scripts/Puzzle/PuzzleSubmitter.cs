using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleSubmitter : InteractableObject
{
    /// <summary>
    /// Attachted Puzzle 
    /// </summary>
    [SerializeField] private PuzzleBase trackedPuzzle;

    /// <summary>
    /// Attacthed timer and dispenser
    /// </summary>
    private PuzzleRewardDispenser rewardDispenser;

    private bool allowDispense;
    private bool dispenseLocked;

    protected override void Start()
    {
        rewardDispenser = GetComponent<PuzzleRewardDispenser>();

        if (trackedPuzzle != null)
            trackedPuzzle.OnPuzzleSolved.AddListener(AllowPlayerReward);

        dispenseLocked = false;
    }
    private void OnDisable()
    {
        if (trackedPuzzle != null)
            trackedPuzzle.OnPuzzleSolved.RemoveListener(AllowPlayerReward);
    }

    /// <summary>
    /// Subscribes and activates when puzzle is correct
    /// </summary>
    private void AllowPlayerReward()
    {
        allowDispense = true;
    }

    //Overrides
    public override void Interact()
    {
        if (trackedPuzzle == null)
            return;

        if (dispenseLocked)
            return;

        //FAIL situation
        if (!allowDispense)
            return;

        rewardDispenser.RetrieveReward();
        dispenseLocked = true;
    }
}
