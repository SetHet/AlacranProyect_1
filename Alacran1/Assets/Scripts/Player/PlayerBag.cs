using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    #region Variables
    public Items items;
    [System.Serializable]
    public class Items
    {
        [System.Serializable]
        public class TypeItem
        {
            [SerializeField] protected ushort current = 0;
            [SerializeField] protected ushort max = 3;

            public ushort getCurrent { get { return current; } }
            public ushort getMax { get { return max; } }
            public bool isEmpty { get { return current <= 0; } }
            public bool isComplete { get { return current >= max; } }

            public ushort Add(ushort cant)
            {
                current += cant;
                if (current > max)
                {
                    ushort exedido = (ushort)(current - max);
                    current = max;
                    return exedido;
                }
                return 0;
            }

            public void Remove(ushort cant)
            {
                int temp = current - cant;
                if (temp < 0) current = 0;
                else current = (ushort)temp;
            }
        }

        public TypeItem botiquin;
        public TypeItem armaduraPlaca;
        public TypeItem ammo_shotgun;
        public TypeItem ammo_pistol;
        public TypeItem ammo_ametralladora;
    }

    public Camera camara;
    public LayerMask itemLayer = 0;
    public float distanceDetectItem = 2f;

    ItemSelected itemSelected = ItemSelected.pistol;
    enum ItemSelected
    {
        pistol,
        ametralladora,
        shotgun,
        botiquin,
        placaArmor
    }

    public GameObject obj_botiquin;
    public GameObject obj_armadura;
    public GameObject obj_pistola;
    public GameObject obj_shotgun;
    public GameObject obj_ametralladora;


    //esto se usaria más adelante
    [System.Serializable]
    public class _Idea_Objetos
    {
        public string name;
        public GameObject obj;
    }
    #endregion

    #region BasicMethods
    private void Start()
    {
        DesabilitarAll();
    }
    private void Update()
    {
        if (PlayerInput.current.GetItem())
        {
            DetectObject();
        }
        SelectItem();
    }
    #endregion

    #region Methods
    void DetectObject()
    {
        RaycastHit hit;
        Ray ray = new Ray(camara.transform.position, camara.transform.forward);

        if (!Physics.Raycast(ray, out hit, distanceDetectItem, itemLayer, QueryTriggerInteraction.Collide)) return;

        Item item = hit.transform.GetComponent<Item>();
        if (item == null) return;

        if (item.tipo == Item.Tipo.Suministro)
        {
            if (item.suministro == Item.Tipo_Suministro.Botiquin)
            {
                item.cantidad = items.botiquin.Add(item.cantidad);
            }
            else if (item.suministro == Item.Tipo_Suministro.PlacaArmadura)
            {
                item.cantidad = items.armaduraPlaca.Add(item.cantidad);
            }

        }
        else if (item.tipo == Item.Tipo.Ammo)
        {
            if (item.ammo == Item.Tipo_Ammo.Shotgun)
            {
                item.cantidad = items.ammo_shotgun.Add(item.cantidad);
            }
            else if (item.ammo == Item.Tipo_Ammo.Pistol)
            {
                item.cantidad = items.ammo_pistol.Add(item.cantidad);
            }
            else if (item.ammo == Item.Tipo_Ammo.Ametralladora)
            {
                item.cantidad = items.ammo_ametralladora.Add(item.cantidad);
            }
        }

        if (item.cantidad <= 0) Destroy(item.gameObject);
    }
    void SelectItem()
    {
        bool change = true;
        if (PlayerInput.current.SelectItemPistol()) itemSelected = ItemSelected.pistol;
        else if (PlayerInput.current.SelectItemAmetralladora()) itemSelected = ItemSelected.ametralladora;
        else if (PlayerInput.current.SelectItemShotgun()) itemSelected = ItemSelected.shotgun;
        else if (PlayerInput.current.SelectItemBotiquin() && !items.botiquin.isEmpty) itemSelected = ItemSelected.botiquin;
        else if (PlayerInput.current.SelectItemPlaca() && !items.armaduraPlaca.isEmpty) itemSelected = ItemSelected.placaArmor;
        else change = false;
        if (change) ChangeSelectItem();
    }
    void ChangeSelectItem()
    {
        DesabilitarAll();
        switch (itemSelected)
        {
            case ItemSelected.pistol:
                if (obj_pistola != null)
                    obj_pistola.SetActive(true);
                break;
            case ItemSelected.ametralladora:
                if (obj_ametralladora != null)
                    obj_ametralladora.SetActive(true);
                break;
            case ItemSelected.shotgun:
                if (obj_shotgun != null)
                    obj_shotgun.SetActive(true);
                break;
            case ItemSelected.botiquin:
                if (obj_botiquin != null)
                    obj_botiquin.SetActive(true);
                break;
            case ItemSelected.placaArmor:
                if (obj_armadura != null)
                    obj_armadura.SetActive(true);
                break;
            default:
                Debug.Log("Change Select Item, no se identifico el objeto");
                break;
        }
    }
    void DesabilitarAll()
    {
        Desabilitar(obj_botiquin);
        Desabilitar(obj_armadura);
        Desabilitar(obj_pistola);
        Desabilitar(obj_ametralladora);
        Desabilitar(obj_shotgun);
    }
    void Desabilitar(GameObject obj)
    {
        if (obj != null) obj.SetActive(false);
    }
    #endregion

}
