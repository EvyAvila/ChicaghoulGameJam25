using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSectionActivator : MonoBehaviour
{
    [SerializeField] private GameObject FlyingArea;

    private void Start()
    {
        GameCurator.OnReachFinalSection += OnActivateSection;
    }
    private void OnDisable()
    {
        GameCurator.OnReachFinalSection -= OnActivateSection;
    }
    private void OnActivateSection()
    {
        if (FlyingArea != null)
            FlyingArea.SetActive(true);
    }
}
