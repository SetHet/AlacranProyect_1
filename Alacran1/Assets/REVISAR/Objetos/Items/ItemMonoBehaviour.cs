using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemMonoBehaviour : MonoBehaviour
{
    public abstract void Out(GameObject root);

    public delegate void HandleEvent();

    public HandleEvent event_out;
    public void EventOut()
    {
        event_out?.Invoke();
    }

    public void _REPAIR_ANIMATOR(Animator animator, string estado_vacio = "empty")
    {
        animator.CrossFade(estado_vacio, 0f);
        animator.Update(0f);
    }
}
