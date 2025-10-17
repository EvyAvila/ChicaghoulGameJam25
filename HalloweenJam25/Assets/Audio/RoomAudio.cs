using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAudio : MonoBehaviour
{
    [SerializeField] private DoorScript EntranceDoor;
    [SerializeField] private DoorScript ExitDoor;
    [SerializeField] private PuzzleSubmitter submitter;

    [Header("Doors")]
    [SerializeField] private AudioClip doorOpenClip;
    [SerializeField] private AudioClip doorCloseClip;

    [Header("Souls")]
    [SerializeField] private AudioClip FailOne;
    [SerializeField] private AudioClip FailTwo;
    [SerializeField] private AudioClip Failure;
    private int failCount;

   [SerializeField] private AudioSource EntranceDoorSource;
   [SerializeField] private AudioSource ExitDoorSource;
   [SerializeField] private AudioSource SubmitSource;

    private void Start()
    {
        EntranceDoor.OnDoorClose += OnEntranceDoorClose;
        ExitDoor.OnDoorOpen += OnExitDoorOpen;
        submitter.OnFailAttempt += OnFailedAttempt;
        submitter.OnFullFailure += Submitter_OnFullFailure;

        EntranceDoorSource.clip = doorCloseClip;
        ExitDoorSource.clip = doorOpenClip;

        failCount = 1;
    }

    private void OnDisable()
    {
        EntranceDoor.OnDoorClose -= OnEntranceDoorClose;
        ExitDoor.OnDoorOpen -= OnExitDoorOpen;
        submitter.OnFailAttempt -= OnFailedAttempt;
        submitter.OnFullFailure -= Submitter_OnFullFailure;
    }
    private void Submitter_OnFullFailure()
    {
        SubmitSource.PlayOneShot(Failure);
    }

    private void OnFailedAttempt()
    {
        if (failCount == 1)
        {
            //SubmitSource.clip = FailOne;
            SubmitSource.PlayOneShot(FailOne);
        }
        else if (failCount == 2)
        {
            //SubmitSource.clip = FailTwo;
            SubmitSource.PlayOneShot(FailTwo);
        }


        failCount++;
    }

    private void OnExitDoorOpen()
    {
        ExitDoorSource.Play();
    }

    private void OnEntranceDoorClose()
    {
        EntranceDoorSource.Play();
    }
}
