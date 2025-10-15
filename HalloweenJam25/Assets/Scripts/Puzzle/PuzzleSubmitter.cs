using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleSubmitter : InteractableObject
{
    /// <summary>
    /// Attacthed Puzzle to update reward claim
    /// </summary>
    public PuzzleBase trackedPuzzle;

    /// <summary>
    /// Attacthed timer and dispenser
    /// </summary>
    private PuzzleRewardDispenser rewardDispenser;

    /// <summary>
    /// Fired event when submission is correct
    /// </summary>
    public UnityEvent OnSuccessfullSubmit;

    //Events
    public static event Action OnSubmitAttemptFail;
    public static event Action OnSubmitFullFailure;

    /// <summary>
    /// The number of times this puzzle was failed
    /// </summary>
    private int failCount;

    private bool allowDispense;
    private bool dispenseLocked;

    protected override void Start()
    {
        failCount = 0;
        rewardDispenser = GetComponent<PuzzleRewardDispenser>();

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

    private void FailSubmit()
    {
        failCount++;

        OnSubmitAttemptFail?.Invoke();

        if (failCount > 2)
        {
            OnSubmitFullFailure?.Invoke();
        }
    }
    public void SetTrackedPuzzle(PuzzleBase instance)
    {
        trackedPuzzle = instance;

        if (trackedPuzzle != null)
            trackedPuzzle.OnPuzzleSolved.AddListener(AllowPlayerReward);
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
        {
            FailSubmit();
            return;
        }

        BloodTracker.AddToBloodLevel(rewardDispenser.RetrieveReward());
        dispenseLocked = true;

        OnSuccessfullSubmit?.Invoke();
    }
}
