using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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

    public event Action OnHoverInteractable;
    public event Action OnGrabInteractable;
    public event Action OnNoneInteractable;
    private bool hasHovered;
    
    private void RayCastForInteractDetection()
    {
        RaycastHit[] res = new RaycastHit[3];

        if (Physics.RaycastNonAlloc(interactPoint.position, interactPoint.forward, res, rayLength, interactMask.value) > 0)
        {
            if (hasHovered)
                return;

            hasHovered = true;
            OnHoverInteractable?.Invoke();
        }
        else
        {

            if (!hasHovered)
                return;

            hasHovered = false;
            OnNoneInteractable?.Invoke();
        }
    }

    private void Update()
    {
        if (currentInteractingItem != null)
            return;

        RayCastForInteractDetection();
    }
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

                //If grabbable, subscribe to forget event
                if (currentInteractingItem.TryGetComponent<GrabbableItem>(out GrabbableItem g))
                {
                    currentInteractingItem.Interact(holdPoint);
                    g.OnForgetItem += SoftStopInteract;
             
                    OnGrabInteractable?.Invoke();
                    hasHovered = false;

                    return;
                }

                currentInteractingItem.Interact();

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
