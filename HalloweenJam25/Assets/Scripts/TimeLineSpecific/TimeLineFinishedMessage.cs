using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineFinishedMessage : MonoBehaviour
{
    public static event Action OnTimeLineFinished;

    public void SignalFinished()
    {
        OnTimeLineFinished?.Invoke();
    }
}
