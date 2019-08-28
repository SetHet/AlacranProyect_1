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

    #endregion

    #region Behaviour
    private void Update()
    {
        Fire();
        Reload();
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
    #endregion

}
