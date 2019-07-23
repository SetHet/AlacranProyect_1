using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireccionCamara : MonoBehaviour
{
    public Transform Camara;
    public Transform Objeto;
    public float Distancia = 1;
    public Dir Direccion = Dir.forward;
    public bool enableLine = true;

    public enum Dir
    {
        forward,
        backward,
        up,
        down,
        right,
        left
    };



    void Update()
    {
        if (!enableLine) return;
        Vector3 obj_dir = Vector3.zero;
        switch (Direccion)
        {
            case Dir.forward:
                obj_dir = Objeto.forward;
                break;
            case Dir.backward:
                obj_dir = -Objeto.forward;
                break;
            case Dir.up:
                obj_dir = Objeto.up;
                break;
            case Dir.down:
                obj_dir = -Objeto.up;
                break;
            case Dir.right:
                obj_dir = Objeto.right;
                break;
            case Dir.left:
                obj_dir = -Objeto.right;
                break;
        }

        Debug.DrawLine(Camara.position, Camara.position + Camara.forward * Distancia, Color.blue);
        Debug.DrawLine(Objeto.position, Objeto.position + obj_dir * Distancia, Color.red);
        Debug.DrawLine(Objeto.position, Objeto.position + Camara.forward * Distancia, Color.green);
    }
}
