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
        {
            StartCoroutine(EndOnBlack());
        }
        else
        {
            StartCoroutine(DefaultEnd());
        }
    }

    private IEnumerator EndOnBlack()
    {
        FadeTransitions.Instance.FadeIn(0.9f);
        yield return new WaitForSecondsRealtime(1);

        UIManager.Instance.DisplayEndingMenu(true);
        FadeTransitions.Instance.FadeOut(0.9f);
        yield return null;
    }
    private IEnumerator DefaultEnd()
    {
        yield return new WaitForSecondsRealtime(1);
        UIManager.Instance.DisplayEndingMenu(true);
        yield return null;
    }
}
