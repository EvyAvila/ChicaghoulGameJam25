using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    [SerializeField] private List<InteractableObject> potionButtons;

    //private bool isInteractable;
    //potionButtons[0].GetComponent<RotatingButtonObjects>().isInteractable = true;


    private void OnEnable()
    {
        PourButtonObject.onInteractionChanged += SetConditions;
    }

    private void OnDisable()
    {
        PourButtonObject.onInteractionChanged -= SetConditions;
    }

    public void SetConditions(float duration)
    {
       SetObjects(false);

        StartCoroutine(Cooldown(duration));
    }

    private IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);

        SetObjects(true);

        StopAllCoroutines();
    }

    private void SetObjects(bool condition)
    {
        potionButtons[0].GetComponent<RotatingButtonObjects>().isInteractable = condition;
        potionButtons[1].GetComponent<RotatingButtonObjects>().isInteractable = condition;
        potionButtons[2].GetComponent<PourButtonObject>().isInteractable = condition;
    }
}
