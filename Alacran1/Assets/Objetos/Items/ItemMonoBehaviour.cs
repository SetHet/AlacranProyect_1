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
}
