using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public bool invertir_movimientoCamara = false;


    private void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        float move_v = Input.GetAxis("Mouse Y") * 0.3f;
        float move_h = Input.GetAxis("Mouse X") * 0.3f;

        Invertir(ref move_v, invertir_movimientoCamara);
        Invertir(ref move_h, invertir_movimientoCamara);

        move_v += animator.GetFloat("moveVertical");
        move_h += animator.GetFloat("moveHorizontal");

        move_v *= (1 - Time.deltaTime * 5);
        move_h *= (1 - Time.deltaTime * 5);

        MinMax(ref move_v);
        MinMax(ref move_h);

        animator.SetFloat("moveVertical", move_v);
        animator.SetFloat("moveHorizontal", move_h);


    }

    void MinMax(ref float x, float min = -1, float max = 1)
    {
        if (x < min) x = min;
        else if (x > max) x = max;
    }

    void Invertir(ref float x, bool invertir)
    {
        if (invertir) x = -x;
    }
}
