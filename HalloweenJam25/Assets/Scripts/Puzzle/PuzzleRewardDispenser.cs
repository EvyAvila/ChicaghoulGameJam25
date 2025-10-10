using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Per puzzle timer component. Determines blood reward based off time spent solving puzzle.
/// </summary>
public class PuzzleRewardDispenser : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] private RoomEnterTrigger srcTrigger;

    [Header("Timer")]
    [SerializeField] private float totalSeconds = 30;
    private float elapsedSeconds;
    private bool countTimer;

    [Header("Blood Reward")]
    [Range(1, 25)]
    [SerializeField] private float maxBloodAmount;
    [Range(1, 25)]
    [SerializeField] private float minBloodAmount;
    private float currentBloodAmount;


    /// <summary>
    /// Visual Blood jar 
    /// </summary>
    private BloodJarVisual bloodJar;


    /// <summary>
    /// The puzzle that this dispenser is connected to
    /// </summary>
    private PuzzleBase trackedPuzzle;

    private void Start()
    {
        elapsedSeconds = 0.0f;
        trackedPuzzle = GetComponent<PuzzleBase>();
        currentBloodAmount = maxBloodAmount;

        //Visual blood
        bloodJar = GetComponent<BloodJarVisual>();

        //Subscriptions
        srcTrigger.triggeredEvent.AddListener(OnTimerTriggered);
    }

    private void OnDisable()
    {
        srcTrigger.triggeredEvent.RemoveListener(OnTimerTriggered);
    }

    public void OnTimerTriggered()
    {
        countTimer = true;
    }
    private void Update()
    {
        if (countTimer)
        {
            if (elapsedSeconds < totalSeconds)
            {
                elapsedSeconds += Time.deltaTime;
            }
            else
            {
                elapsedSeconds = totalSeconds;
                countTimer = false;

                Debug.Log("Puzzle timer finished");
            }

            currentBloodAmount = Mathf.Lerp(maxBloodAmount, minBloodAmount, timeToRatio());
        
            if (bloodJar != null)
            {
                bloodJar.SetShaderFill(currentBloodAmount/maxBloodAmount);
            }
        }
    }

    private float timeToRatio()
    {
        return Mathf.Clamp01(elapsedSeconds / totalSeconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (countTimer)
            return;

        countTimer = true;
    }
}
