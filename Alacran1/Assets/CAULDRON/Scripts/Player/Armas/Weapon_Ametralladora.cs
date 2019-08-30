using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Ametralladora : MonoBehaviour
{
    #region 
    [Header("Interaccion")]
    public Transform DireccionDaño;
    public float daño = 40f;
    public float distanciaMaxima = 100f;
    public LayerMask layerDaño = -1;
    
    [Header("Sonido")]
    public AudioSource audioFire;
    public AudioSource audioReload;
    public AudioClip audioClipReaload_A;
    public AudioClip audioClipReaload_B;
    #endregion


    #region Metodos De Animacion
    public void Fire_Ametralladora()
    {
        Atack();
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

    #region Methods
    void Atack()
    {
        Vector3 origen = DireccionDaño.position;
        Vector3 direccion = DireccionDaño.forward;
        RaycastHit hit;
        Debug.Log("Atack 0");
        if (Physics.Raycast(origen, direccion, out hit, distanciaMaxima, layerDaño, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("Atack 1");
            InterfaceDamage obj = hit.collider.GetComponent<InterfaceDamage>();
            if (obj != null)
            {
                Debug.Log("Atack 2");
                obj.Damage(daño);
            }
        }
    }
    #endregion
}
