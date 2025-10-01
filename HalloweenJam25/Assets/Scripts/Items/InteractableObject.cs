using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
    public virtual void Interact() { }
    public virtual void Interact(Transform t) { Interact(); }
    public virtual void StopInteraction() { }
}
