using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Ametralladora : MonoBehaviour
{
    #region 
    public AudioSource audioFire;
    public AudioSource audioReload;
    public AudioClip audioClipReaload_A;
    public AudioClip audioClipReaload_B;
    #endregion


    #region Metodos De Animacion
    public void Fire_Ametralladora()
    {
        audioFire.Play();
    }
    
    public void Reload_Ametralladora_1()
    {
        audioReload.clip = audioClipReaload_A;
        audioReload.Play();
    }
    public void Reload_Ametralladora_2()
    {
        audioReload.clip = audioClipReaload_B;
        audioReload.Play((ulong)0.6f);
    }
    
    #endregion
}
