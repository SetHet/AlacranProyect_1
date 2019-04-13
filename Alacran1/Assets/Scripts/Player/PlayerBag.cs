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
    #endregion

    #region BasicMethods
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
        if (PlayerInput.current.SelectItemPistol()) itemSelected = ItemSelected.pistol;
        else if (PlayerInput.current.SelectItemAmetralladora()) itemSelected = ItemSelected.ametralladora;
        else if (PlayerInput.current.SelectItemShotgun()) itemSelected = ItemSelected.shotgun;
        else if (PlayerInput.current.SelectItemBotiquin()) itemSelected = ItemSelected.botiquin;
        else if (PlayerInput.current.SelectItemPlaca()) itemSelected = ItemSelected.placaArmor;
    }
    #endregion

}
