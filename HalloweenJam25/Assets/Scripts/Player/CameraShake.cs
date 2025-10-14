using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    [Header("Shake Timer")]
    [SerializeField] private float shakeTimer = 0.8f;
    private float currentTime;

    [Header("Shake amount")]
    [SerializeField] private float maxShakeAmount;
    private float shakeAmount;
    private bool shake;
    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        currentTime = 0.0f;

        //Subscriptions
        PuzzleSubmitter.OnSubmitAttemptFail += OnSubmitFail;
    }
    private void OnDisable()
    {
        PuzzleSubmitter.OnSubmitAttemptFail -= OnSubmitFail;
    }
    private void OnSubmitFail()
    {
        shake = true;
        currentTime = 0.0f;
        shakeAmount = maxShakeAmount;
    }

    private void Update()
    {
        if (!shake)
            return;

        currentTime += Time.deltaTime;

        perlinNoise.m_AmplitudeGain = Mathf.Lerp(maxShakeAmount, 0.0f, currentTime/shakeTimer);

        if (currentTime > shakeTimer)
        {
            shake = false;
            shakeAmount = 0.0f;
        }
    }

}
