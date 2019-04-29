using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : InteractBehaviour
{
    public float offset;

    bool open;
    public override void Interact()
    {
        float angle;
        if (open)
        {
            angle = -90;
        }
        else
        {
            angle = 90;
        }
        transform.RotateAround(transform.position+transform.right*offset,transform.up,angle);
        open = !open;
    }
}
