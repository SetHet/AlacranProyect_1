using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraccion : MonoBehaviour
{
    #region Variables
    public Camera camara;
    public float distanceDetectItem = 2;
    public LayerMask interactLayer = -1;
    #endregion
    
    void Update()
    {
        if (!PlayerInput.current.GetItem()) return;

        RaycastHit hit;
        Ray ray = new Ray(camara.transform.position, camara.transform.forward);

        if (!Physics.Raycast(ray, out hit, distanceDetectItem, interactLayer, QueryTriggerInteraction.Collide)) return;

        InteractBehaviour interact = hit.collider.GetComponent<InteractBehaviour>();
        if (interact == null) return;

        interact.Interact();
    }
}
