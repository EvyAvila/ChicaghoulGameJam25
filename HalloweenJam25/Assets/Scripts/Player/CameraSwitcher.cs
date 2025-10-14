using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera GroundCamera;
    [SerializeField] private CinemachineVirtualCamera FlightCamera;
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
        GroundCamera.gameObject.SetActive(false);
        FlightCamera.gameObject.SetActive(true);
    }
}
