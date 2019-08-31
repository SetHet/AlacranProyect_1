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
    public float minRecoil = 0.05f;
    public float maxRecoil = 0.2f;

    [Header("Sonido")]
    public AudioSource audioFire;
    public AudioSource audioReload;
    public AudioClip audioClipReaload_A;
    public AudioClip audioClipReaload_B;

    //private 
    public Animator animator;
    public string moveVertical = "moveVertical";
    public string moveHorizotal = "moveHorizontal";
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

        Recoil();

    }

    void Recoil()
    {
        Vector2 move = new Vector2();

        move.x = Random.Range(-maxRecoil, maxRecoil);
        move.y = Random.Range(-maxRecoil, maxRecoil);

        if (move.magnitude > maxRecoil)
        {
            move = move.normalized * maxRecoil;
        }
        else if (move.magnitude < minRecoil)
        {
            move = move.normalized * minRecoil;
        }

        move.x += animator.GetFloat(moveHorizotal);
        move.y += animator.GetFloat(moveVertical);

        if (move.magnitude > 1) move = move.normalized;

        animator.SetFloat(moveHorizotal, move.x);
        animator.SetFloat(moveVertical, move.y);
    }

    
    #endregion
}
