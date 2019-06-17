using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractBehaviour : MonoBehaviour
{
    public abstract void Interact();
    public virtual void Start()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Interact"))
        {
            Debug.LogWarning("Este Objeto no tiene el layer Interact");
        }
        
    }
}
