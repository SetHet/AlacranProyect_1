using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ametralladora_T : MonoBehaviour
{
    #region Variables
    [SerializeField] protected string param_reload = "Reload";
    [SerializeField] protected string param_fire = "Fire";
    [SerializeField] protected string param_out = "Out";

    Animator animator;
    bool reloading = false;
    #endregion


    #region Basic Methods
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (reloading) return;
        if (PlayerInput.current.GetReloadDown())
        {
            Fire(false);
            Reload();
        }
        else if (PlayerInput.current.GetFire())
        {
            Fire(true);
        }
        else
        {
            Fire(false);
        }
    }
    #endregion


    #region Methods
    void Fire(bool enabled)
    {
        animator.SetBool(param_fire, enabled);
    }
    void Reload()
    {
        reloading = true;
        animator.SetTrigger(param_reload);
    }
    public void Out()
    {
        animator.SetTrigger(param_out);
    }
    #endregion
    #region Events Animation

    public delegate void HandleEvent();
    
    public HandleEvent event_out;
    public void EventOut()
    {
        event_out?.Invoke();
    }

    public HandleEvent event_reload;
    public void EventReload()
    {
        reloading = false;
        event_reload?.Invoke();
    }

    #endregion
}
