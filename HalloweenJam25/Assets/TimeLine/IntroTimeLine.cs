using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class IntroTimeLine : MonoBehaviour
{
    private PlayableDirector tmDirector;

    public UnityEvent OnIntroFinish;
    private void Start()
    {
        tmDirector = GetComponent<PlayableDirector>();

        tmDirector.stopped += OnIntroFinished;
    }
    private void OnDisable()
    {
        tmDirector.stopped -= OnIntroFinished;
    }
    private void OnIntroFinished(PlayableDirector obj)
    {
        Debug.Log("Intro Finished");
        OnIntroFinish?.Invoke();
        gameObject.SetActive(false);
    }
}
