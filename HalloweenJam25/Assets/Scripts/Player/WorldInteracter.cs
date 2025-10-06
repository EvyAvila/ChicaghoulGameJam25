using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WorldInteracter : MonoBehaviour
{
    [Header("Interact Raycasting")]
    [SerializeField] private Transform interactPoint;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float rayLength = 3.0f;
    [SerializeField] private LayerMask interactMask;

    /// <summary>
    /// Reference to currently interacted item
    /// </summary>
    private InteractableObject currentInteractingItem;
    public void CheckForInteract()
    {
        if (interactPoint == null)
            return;

        Debug.DrawRay(interactPoint.transform.position, interactPoint.forward * rayLength, Color.red);

        RaycastHit[] res = new RaycastHit[3];
        if (Physics.RaycastNonAlloc(interactPoint.position, interactPoint.forward, res, rayLength, interactMask.value) > 0)
        {
            GameObject obj = res[0].collider.gameObject;

            if (obj.TryGetComponent<InteractableObject>(out InteractableObject interactable))
            {
                currentInteractingItem = interactable;
                currentInteractingItem.Interact(holdPoint);

                //If grabbable, subscribe to forget event
                if (currentInteractingItem.TryGetComponent<GrabbableItem>(out GrabbableItem g))
                {
                    g.OnForgetItem += SoftStopInteract;
                }
            }
        }
    }

    public void CheckForSecondaryInteract()
    {
        Debug.DrawRay(interactPoint.transform.position, interactPoint.forward * rayLength, Color.green);

        RaycastHit[] res = new RaycastHit[3];
        if (Physics.RaycastNonAlloc(interactPoint.position, interactPoint.forward, res, rayLength, interactMask.value) > 0)
        {
            GameObject obj = res[0].collider.gameObject;

            if (obj.TryGetComponent<InteractableObject>(out InteractableObject interactable))
            {
                interactable.SecondaryInteract();
            }
        }
    }

    private void SoftStopInteract()
    {
        currentInteractingItem.GetComponent<GrabbableItem>().OnForgetItem -= SoftStopInteract;
        currentInteractingItem = null;
    }

    public void StopInteracting()
    {
        if (currentInteractingItem == null)
            return;

        //If grabbable, unsubscribe
        if (currentInteractingItem.TryGetComponent<GrabbableItem>(out GrabbableItem g))
        {
            g.OnForgetItem -= SoftStopInteract;
        }

        currentInteractingItem.StopInteraction();

        //Stop tracking the item
        currentInteractingItem = null;
    }
}
