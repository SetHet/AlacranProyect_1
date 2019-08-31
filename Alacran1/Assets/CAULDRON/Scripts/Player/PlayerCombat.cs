using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    #region Variables
    public PlayerBag bag;
    public Animator animator;

    [Header("Id Animator")]
    public string anim_fire = "fire";
    public string anim_reload = "reload";
    public string anim_enable = "active";
    public string anim_aim = "aim";

    [Header("Apuntar")]
    public float VelocidadApuntado = 3f;
    public float VelocidadDesapuntar = 3f;

    //Privados
    float aimValue = 0f;

    #endregion

    #region Behaviour
    private void Update()
    {
        Fire();
        Reload();
        Aim();
    }

    #endregion

    #region Methods
    void Fire()
    {
        if (PlayerInput.current.GetFire())
        {
            animator.SetBool(anim_fire, true);
        }
        else animator.SetBool(anim_fire, false);
    }

    void Reload()
    {
        if (PlayerInput.current.GetReloadDown())
        {
            animator.SetTrigger(anim_reload);
        }
    }

    void Aim()
    {
        if (PlayerInput.current.GetAim())
        {
            aimValue += Time.deltaTime * VelocidadApuntado;
            if (aimValue > 1) aimValue = 1;
        }
        else
        {
            aimValue -= Time.deltaTime * VelocidadDesapuntar;
            if (aimValue < 0) aimValue = 0;
        }
        animator.SetFloat(anim_aim, aimValue);
    }
    #endregion

}
