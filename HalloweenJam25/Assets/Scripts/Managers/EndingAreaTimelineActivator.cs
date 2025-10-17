using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAreaTimelineActivator : MonoBehaviour
{
    [SerializeField] private GameObject SuperEndingObj;
    [SerializeField] private GameObject RegularEndingObj;
    [SerializeField] private GameObject BadEndingObj;

    private void Start()
    {
        GameCurator.OnCastleDoorReached += OnFlyingSectionEnded;
    }

    private void OnDisable()
    {
        GameCurator.OnCastleDoorReached -= OnFlyingSectionEnded;
    }

    private void OnFlyingSectionEnded()
    {
        switch (GameEndingPicker.Instance.ending)
        {
            case EndingType.BAD:
                BadEndingObj.SetActive(true);
                break;
            case EndingType.NORMAL:
                RegularEndingObj.SetActive(true);
                break;
            case EndingType.SUPER:
                SuperEndingObj.SetActive(true);
                break;
            default:
                break;
        }

    }
}
