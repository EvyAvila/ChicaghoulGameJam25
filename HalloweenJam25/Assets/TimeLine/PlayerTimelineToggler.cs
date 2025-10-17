using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimelineToggler : MonoBehaviour
{
    [SerializeField] private GameObject TimerLossTimeline;
    [SerializeField] private GameObject SoulLossTimeline;

    private void Start()
    {
        GameCurator.OnLoseGameSouls += OnSoulsFailure;
        GameCurator.OnLoseGameSun += OnTimeFailure;
    }
    private void OnDisable()
    {
        GameCurator.OnLoseGameSouls -= OnSoulsFailure;
        GameCurator.OnLoseGameSun -= OnTimeFailure;
    }
    private void OnTimeFailure()
    {
        TimerLossTimeline.SetActive(true);
    }

    private void OnSoulsFailure()
    {
        SoulLossTimeline.SetActive(true);
    }
}
