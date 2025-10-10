using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class TestInteract : InteractableObject
{
    [SerializeField] private Material materialOne;
    [SerializeField] private Material materialTwo;
    private bool switched;

    private void Start()
    {
        SessionTimer.OnTimerFinished += GameFinished;
    }
    private void OnDisable()
    {
        SessionTimer.OnTimerFinished -= GameFinished;
    }

    private void GameFinished()
    {
        gameObject.GetComponent<Renderer>().material = materialTwo;
    }

    public override void Interact()
    {
        if (switched)
        {
            gameObject.GetComponent<Renderer>().material = materialOne;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = materialTwo;
        }

        switched = !switched;
    }
}
