using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapBehaviuor : MonoBehaviour
{
    #region Variables
    public string tagPlayer = "Player";
    #endregion

    protected virtual bool DetectPlayer(GameObject obj)
    {
        return obj.tag == tagPlayer;
    }
    
}
