using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ametralladora_T : ItemMonoBehaviour
{
    #region Variables Configuracion
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

    #region Variables Almacenamiento
    public PlayerBag bag;
    public int municion = 0;
    public int municion_maxima = 50;
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
        if (PlayerInput.current.GetReloadDown() && !bag.items.ammo_ametralladora.isEmpty)
        {
            Fire(false);
            Reload();
        }
        else if (PlayerInput.current.GetFire() && municion > 0)
        {
            Fire(true);
        }
        else
        {
            Fire(false);
        }
    }
    private void OnGUI()
    {
        string mensaje = "Amet: " + municion + "/" + municion_maxima + "><bag:" + bag.items.ammo_ametralladora.getCurrent;
        GUI.Label(new Rect(Screen.width - 100, Screen.height - 20, 100, 20), mensaje);
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
        Bag_Reload();
    }
    public override void Out(GameObject _root)
    {
        root = _root;
        animator.SetTrigger(param_out);
    }
    void inOut()
    {
        _REPAIR_ANIMATOR(animator, "empty");
        root.SetActive(false);
        
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
    #region Methods Almacenamiento
    void Bag_Fire()
    {
        municion--;
        if (municion <= 0) Fire(false);
    }
    void Bag_Reload()
    {
        int cantidad_bag = bag.items.ammo_ametralladora.getCurrent;
        int necesita = municion_maxima - municion;

        if (cantidad_bag > necesita)
        {
            municion = municion_maxima;
            bag.items.ammo_ametralladora.Remove((ushort)necesita);
        }
        else
        {
            municion += cantidad_bag;
            bag.items.ammo_ametralladora.Remove((ushort)cantidad_bag);
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
        Bag_Fire();
        event_fire?.Invoke();
    }

    #endregion
}
