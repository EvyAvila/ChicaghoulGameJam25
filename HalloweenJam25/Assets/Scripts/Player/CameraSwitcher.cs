using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera GroundCamera;
    [SerializeField] private CinemachineVirtualCamera FlightCamera;

    public static event Action OnFlyingTransitionFinished;
    private void Start()
    {
        GameCurator.OnReachFinalSection += GameCurator_OnReachFinalSection;
    }
    private void OnDisable()
    {
        GameCurator.OnReachFinalSection -= GameCurator_OnReachFinalSection;
    }
    private void GameCurator_OnReachFinalSection()
    {
        StartCoroutine(EnterFlyingSection());
        //GroundCamera.gameObject.SetActive(false);
        //FlightCamera.gameObject.SetActive(true);
    }

    private IEnumerator EnterFlyingSection()
    {
        FadeTransitions.Instance.FadeIn(1.5f);
        yield return new WaitForSecondsRealtime(1.5f);
    
        GroundCamera.gameObject.SetActive(false);
        FlightCamera.gameObject.SetActive(true);
        OnFlyingTransitionFinished?.Invoke();
        yield return new WaitForSecondsRealtime(1.5f);
        FadeTransitions.Instance.FadeOut(1.5f);

        yield return null; 
    }
}
