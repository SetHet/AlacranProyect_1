using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseBlock : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (Cursor.lockState == CursorLockMode.None) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }
    }
}