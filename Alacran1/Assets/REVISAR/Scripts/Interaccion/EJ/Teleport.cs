using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : TrapBehaviuor
{
    [SerializeField] private Vector3 pos = new Vector3(0, 2, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (!DetectPlayer(other.gameObject)) return;

        other.gameObject.GetComponent<CharacterController>().enabled = false;
        other.transform.position = pos;
        other.gameObject.GetComponent<CharacterController>().enabled = true;
    }
}
