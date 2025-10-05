using System;
using UnityEngine;
using UnityEngine.Events;

//From reference b3agz - https://youtu.be/gIjajeyjRfE?si=Eke4NzpUOlaCn9tu

[Serializable]
public class NoteDivision 
{
    [SerializeField] private float steps;
    [SerializeField] private UnityEvent IntervalEvent;
    private float lastInterval;
    public float GetIntervalLength(float bpm)
    {
        return 60 / bpm*steps;
    }

    public void CheckForNewInterval(float interval)
    {
        float floored = Mathf.FloorToInt(interval);

        if (floored != lastInterval)
        {
            lastInterval = floored;
            IntervalEvent.Invoke();
        }
    }
}
