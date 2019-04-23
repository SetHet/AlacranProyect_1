using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ametralladora_T : ItemMonoBehaviour
{
    #region Variables
    [SerializeField] protected string param_reload = "Reload";
    [SerializeField] protected string param_fire = "Fire";
    [SerializeField] protected string param_out = "Out";

    [System.Serializable] public struct audioClipList
    {
        public string name;
        public AudioClip clip;
    }
    public List<audioClipList> sounds = new List<audioClipList>();
    [SerializeField]
    protected AudioSource source;

    Animator animator;
    bool reloading = false;
    GameObject root;
    #endregion


    #region Basic Methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
        event_out += inOut;
    }
    private void OnEnable()
    {
        
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
    public override void Out(GameObject _root)
    {
        root = _root;
        animator.SetTrigger(param_out);
    }
    void inOut()
    {
        root.SetActive(false);
        animator.Play("In", 0);
    }
    public void CallSound(string nameClip)
    {
        foreach (var item in sounds)
        {
            if (item.name != nameClip) continue;
            source.clip = item.clip;
            source.Play();
            break;
        }
    }

    #endregion
    #region Events Animation
    
    public HandleEvent event_reload;
    public void EventReload()
    {
        reloading = false;
        event_reload?.Invoke();
    }

    public HandleEvent event_fire;
    public void EventFire()
    {
        event_fire?.Invoke();
    }

    #endregion
}
