using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private WorldInteracter interacter;

    private const string DefaultName = "DefaultArm";
    private const string HoverStartName = "ArmSeekInteract";
    private const string GrabStartName = "ArmGrabStart";

    private int DefaultHash;
    private int HoverStartHash;
    private int GrabStartHash;

    private void Start()
    {
        interacter = GetComponent<WorldInteracter>();

        DefaultHash = Animator.StringToHash(DefaultName);
        HoverStartHash = Animator.StringToHash(HoverStartName);
        GrabStartHash = Animator.StringToHash(GrabStartName);

        interacter.OnGrabInteractable += Interacter_OnGrabInteractable;
        interacter.OnHoverInteractable += Interacter_OnHoverInteractable;
        interacter.OnNoneInteractable += Interacter_OnNoneInteractable;
    }
    private void OnDisable()
    {
        if (interacter != null)
        {
            interacter.OnGrabInteractable -= Interacter_OnGrabInteractable;
            interacter.OnHoverInteractable -= Interacter_OnHoverInteractable;
            interacter.OnNoneInteractable -= Interacter_OnNoneInteractable;
        }
    }

    private void Interacter_OnNoneInteractable()
    {
        animator.CrossFade(DefaultHash,0.1f);
    }

    private void Interacter_OnHoverInteractable()
    {
        animator.CrossFade(HoverStartHash,0.1f);
    }
    private void Interacter_OnGrabInteractable()
    {
        animator.CrossFade(GrabStartHash, 0.1f);
    }
}
