using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameCurator : MonoBehaviour
{
    public static GameCurator Instance { get; private set; }

    //Events
    public static event Action OnWindowEscape;
    public static event Action OnReachFinalSection;
    public static event Action OnCastleDoorReached;
    public static event Action OnLoseGameSun;
    public static event Action OnLoseGameSouls;
    public static event Action OnDecideEnding;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        EscapeObject.OnPlayerEscaped += OnEscaped;
        SessionTimer.OnTimerFinished += OnGameLostByTimer;
        PuzzleSubmitter.OnSubmitFullFailure += OnGameLostBySouls;
        FinalSectionTrigger.OnReachFinal += OnFinalSection;
    }
    private void OnDisable()
    {
        EscapeObject.OnPlayerEscaped -= OnEscaped;
        SessionTimer.OnTimerFinished -= OnGameLostByTimer;
        PuzzleSubmitter.OnSubmitFullFailure -= OnGameLostBySouls;
        FinalSectionTrigger.OnReachFinal -= OnFinalSection;
    }
    private void OnGameLostByTimer()
    {
        Debug.Log("Lost by TIME");
        OnLoseGameSun?.Invoke();
    }

    private void OnGameLostBySouls()
    {
        Debug.Log("Lost by souls");
        OnLoseGameSouls?.Invoke();
    }

    private void OnEscaped(bool usingCollider)
    {
        OnDecideEnding?.Invoke();

        if (usingCollider)
        {
            Debug.Log("Reach castle door");
            OnCastleDoorReached?.Invoke();
        }
        else
        {
            Debug.Log("Escaped via window");
            OnWindowEscape?.Invoke();
        }

    }

    private void OnFinalSection()
    {
        Debug.Log("Triggered Final Section");
        OnReachFinalSection?.Invoke();
    }

    //Non event methods
    private void SwitchToEnding()
    {

    }
}
