using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : InteractBehaviour
{
    #region Variables
    public string IDVariableColor;
    #endregion

    public override void Interact()
    {
        Material material = gameObject.GetComponent<MeshRenderer>().materials[0];

        material.SetColor(IDVariableColor, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1));
    }
}
