using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrack : MonoBehaviour
{
    [SerializeField] private float trackSpeed;
    [SerializeField]private bool moveTrack;

    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();

        CameraSwitcher.OnFlyingTransitionFinished += CameraSwitcher_OnFlyingTransitionFinished;
    }
    private void OnDisable()
    {
        CameraSwitcher.OnFlyingTransitionFinished += CameraSwitcher_OnFlyingTransitionFinished;
    }

    private void CameraSwitcher_OnFlyingTransitionFinished()
    {
        StartTrack();
    }

    private void Update()
    {
        if (!moveTrack)
            return;

        transform.localPosition += Vector3.back * trackSpeed * Time.deltaTime;
    }

    private void StartTrack()
    {
        moveTrack = true;
        source.Play();
    }
}
