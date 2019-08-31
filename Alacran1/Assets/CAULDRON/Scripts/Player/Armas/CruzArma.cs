using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruzArma : MonoBehaviour
{
    #region Variables
    public Camera camera;
    public Transform weaponForward;
    public RectTransform uiPoint;
    public float distance = 50f;
    public LayerMask layers = -1;
    #endregion

    #region Methods Behaviour
    private void Update()
    {
        if (Close())
        {
            Debug.Log("Faltan parametros");
            return;
        }

        Vector3 inicio = weaponForward.position;
        Vector3 direccion = weaponForward.forward;
        RaycastHit hit;
        if (Physics.Raycast(inicio, direccion, out hit, distance, layers, QueryTriggerInteraction.Ignore))
        {
            MoverPuntero(hit.point);
        }
        else
        {
            MoverPuntero(inicio + direccion * distance);
        }
    }
    #endregion

    #region Methods
    bool Close() => camera == null || weaponForward == null || uiPoint == null;
    void MoverPuntero(Vector3 posicion)
    {
         uiPoint.SetPositionAndRotation(camera.WorldToScreenPoint(posicion), Quaternion.identity);
    }
    #endregion
}
