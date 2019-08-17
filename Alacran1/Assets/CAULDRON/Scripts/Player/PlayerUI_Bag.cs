using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI_Bag : MonoBehaviour
{
    public UIO UIObjects;
    [System.Serializable]
    public class UIO
    {
        public Text botiquin;
        public Text placaArmor;
        public Text ammo_shotgun;
        public Text ammo_pistol;
        public Text ammo_ametralladora;

        public GameObject obj_shotgun;
        public GameObject obj_pistol;
        public GameObject obj_ametralladora;

        public GameObject zonaSelect;
        public GameObject zonaOther;

        public float ScaleOther = 0.7f;
    }

    public PlayerBag bag;
    
    private void Update()
    {
        //UIObjects.botiquin.text = bag.items.botiquin.getCurrent.ToString() + "/" + bag.items.botiquin.getMax.ToString();
        //UIObjects.placaArmor.text = bag.items.armaduraPlaca.getCurrent.ToString() + "/" + bag.items.armaduraPlaca.getMax.ToString();
        UIObjects.ammo_shotgun.text = bag.items.ammo_shotgun.getCurrent.ToString() + "/" + bag.items.ammo_shotgun.getMax.ToString();
        UIObjects.ammo_pistol.text = bag.items.ammo_pistol.getCurrent.ToString() + "/" + bag.items.ammo_pistol.getMax.ToString();
        UIObjects.ammo_ametralladora.text = bag.items.ammo_ametralladora.getCurrent.ToString() + "/" + bag.items.ammo_ametralladora.getMax.ToString();

        ColocarInNotSelect(UIObjects.obj_ametralladora);
        ColocarInNotSelect(UIObjects.obj_pistol);
        ColocarInNotSelect(UIObjects.obj_shotgun);

        switch (bag.itemSelected)
        {
            case PlayerBag.ItemSelected.pistol:
                ColocarInSelect(UIObjects.obj_pistol);
                break;
            case PlayerBag.ItemSelected.ametralladora:
                ColocarInSelect(UIObjects.obj_ametralladora);
                break;
            case PlayerBag.ItemSelected.shotgun:
                ColocarInSelect(UIObjects.obj_shotgun);
                break;
            case PlayerBag.ItemSelected.botiquin:
                break;
            case PlayerBag.ItemSelected.placaArmor:
                break;
            default:
                break;
        }
    }

    void ColocarInSelect(GameObject obj)
    {
        RectTransform rec = obj.GetComponent<RectTransform>();
        rec.SetParent(UIObjects.zonaSelect.transform);
        rec.localScale = new Vector3(1, 1, 1);
    }

    void ColocarInNotSelect(GameObject obj)
    {
        RectTransform rec = obj.GetComponent<RectTransform>();
        rec.SetParent(UIObjects.zonaOther.transform);
        rec.localScale = new Vector3(1, 1, 1) * UIObjects.ScaleOther;
    }
}
