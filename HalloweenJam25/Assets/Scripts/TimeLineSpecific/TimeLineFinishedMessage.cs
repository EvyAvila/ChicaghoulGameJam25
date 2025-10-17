using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineFinishedMessage : MonoBehaviour
{
    [SerializeField] private bool blackOutOnFinish;

    public static event Action OnTimeLineFinished;
    public static event Action OnBlackOutFinish;

    public void SignalFinished()
    {
        OnTimeLineFinished?.Invoke();
        
        if (blackOutOnFinish)
            OnBlackOutFinish?.Invoke();
    }
}
