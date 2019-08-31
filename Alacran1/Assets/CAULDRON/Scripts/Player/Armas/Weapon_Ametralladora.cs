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

    public Animator animator;
    public string moveVertical = "moveVertical";
    public string moveHorizotal = "moveHorizontal";

    [Header("Hole")]
    public GameObject prefabHole;
    public float TimeDestroy = 10f;
    public float SizeMax = 0.05f;
    public float SizeMin = 0.02f;
    //private 
    float coolDownFire = 0.001f;
    float time_coolDownFire = 0f;
    #endregion




    #region Metodos De Animacion
    public void Fire_Ametralladora()
    {
        if (!DireccionDaño.gameObject.activeInHierarchy) return;
        if (time_coolDownFire > Time.time) return;
        time_coolDownFire = Time.time + coolDownFire;

        Atack();
        audioFire.Play();
    }
    
    public void Reload_Ametralladora_1()
    {
        if (!DireccionDaño.gameObject.activeInHierarchy) return;
        audioReload.clip = audioClipReaload_A;
        audioReload.Play();
    }
    public void Reload_Ametralladora_2()
    {
        if (!DireccionDaño.gameObject.activeInHierarchy) return;
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
        if (Physics.Raycast(origen, direccion, out hit, distanciaMaxima, layerDaño, QueryTriggerInteraction.Ignore))
        {
            InterfaceDamage obj = hit.collider.GetComponent<InterfaceDamage>();
            if (obj != null)
            {
                obj.Damage(daño);
            }
            CreateHole(hit);
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

    void CreateHole(RaycastHit hit)
    {
        if (prefabHole == null) return;
        GameObject obj = Instantiate(prefabHole, hit.point + hit.normal * 0.001f, Quaternion.identity);

        obj.transform.forward = hit.normal;

        Vector2 size = new Vector2(Random.Range(SizeMin, SizeMax), Random.Range(SizeMin, SizeMax));
        obj.transform.localScale = size;
        obj.transform.parent = hit.collider.transform;
        Destroy(obj, TimeDestroy);
    }
    
    #endregion
}
