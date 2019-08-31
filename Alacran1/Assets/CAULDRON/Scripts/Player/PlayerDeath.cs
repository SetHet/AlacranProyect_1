using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public float alturaDeath = -10;

    void Update()
    {
        if (PlayerStats.current.isDeath()) Death();
        if (PlayerStats.current.transform.position.y < alturaDeath) Death();
    }

    void Death()
    {
        //SceneManager.UnloadScene(0);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
