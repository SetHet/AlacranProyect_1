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
    }

    public PlayerBag bag;
    
    private void Update()
    {
        UIObjects.botiquin.text = bag.items.botiquin.getCurrent.ToString() + "/" + bag.items.botiquin.getMax.ToString();
        UIObjects.placaArmor.text = bag.items.armaduraPlaca.getCurrent.ToString() + "/" + bag.items.armaduraPlaca.getMax.ToString();
        UIObjects.ammo_shotgun.text = bag.items.ammo_shotgun.getCurrent.ToString() + "/" + bag.items.ammo_shotgun.getMax.ToString();
        UIObjects.ammo_pistol.text = bag.items.ammo_pistol.getCurrent.ToString() + "/" + bag.items.ammo_pistol.getMax.ToString();
        UIObjects.ammo_ametralladora.text = bag.items.ammo_ametralladora.getCurrent.ToString() + "/" + bag.items.ammo_ametralladora.getMax.ToString();
    }
}
