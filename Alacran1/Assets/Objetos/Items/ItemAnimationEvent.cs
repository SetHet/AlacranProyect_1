using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimationEvent : MonoBehaviour
{
    public delegate void HandleEvent();

    public HandleEvent event_out;
    public void Out()
    {
        event_out?.Invoke();
    }

    public HandleEvent event_reload;
    public void Reload()
    {
        event_reload?.Invoke();
    }
}
