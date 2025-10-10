using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionTimer : MonoBehaviour
{
    [SerializeField] private float totalSeconds;
    private float elapsedSeconds;
    private bool countTimer;
    public static SessionTimer Instance { get; private set; }

    //Events
    public static event Action OnTimerFinished;

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
        countTimer = false;
        elapsedSeconds = 0.0f;

        //Subscriptions
        SessionTrigger.OnSessionTriggerEnter += OnTimerStart;
    }
    private void OnDisable()
    {
        SessionTrigger.OnSessionTriggerEnter -= OnTimerStart;
    }
    private void OnTimerStart()
    {
        countTimer = true;
    }

    private void Update()
    {
        if (countTimer)
        {
            if (elapsedSeconds < totalSeconds)
            {
                totalSeconds += Time.deltaTime;
            }
            else
            {
                elapsedSeconds = totalSeconds;
                countTimer = false;
                OnTimerFinished?.Invoke();
            }
        }
        
    }
}
