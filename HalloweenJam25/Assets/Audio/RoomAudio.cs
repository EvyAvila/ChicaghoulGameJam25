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

    private AudioSource source;

    private void Start()
    {
        EntranceDoor.OnDoorClose += OnEntranceDoorClose;
        ExitDoor.OnDoorOpen += OnExitDoorOpen;
        submitter.OnFailAttempt += OnFaileAttempt;
        submitter.OnFullFailure += Submitter_OnFullFailure;
    }

    private void OnDisable()
    {
        EntranceDoor.OnDoorClose -= OnEntranceDoorClose;
        ExitDoor.OnDoorOpen -= OnExitDoorOpen;
        submitter.OnFailAttempt -= OnFaileAttempt;
        submitter.OnFullFailure -= Submitter_OnFullFailure;
    }
    private void Submitter_OnFullFailure()
    {
        
    }

    private void OnFaileAttempt()
    {
    
    }

    private void OnExitDoorOpen()
    {

    }

    private void OnEntranceDoorClose()
    {

    }
}
