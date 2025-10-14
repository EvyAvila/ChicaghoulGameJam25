using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

public class BatBlowUp : MonoBehaviour
{
    [SerializeField] private GameObject physicalBody;
    [SerializeField] private ParticleSystem particles;
    private SplineAnimate splineDriver;
    private bool finished;

    public event Action OnLowBloodEndingFinished;
    private void Start()
    {
        finished = false;
        splineDriver = GetComponent<SplineAnimate>();
    }

    private void Update()
    {
        if (finished)
            return;

        if (!splineDriver.IsPlaying )
        {
            Debug.Log("FInsihed SPline");
            OnLowBloodEndingFinished?.Invoke();
            particles.Play();
            physicalBody.SetActive(false);
            finished = true;
        }
    }
}
