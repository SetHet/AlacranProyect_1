using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI_VidaAndArmor : MonoBehaviour
{

    [System.Serializable]
    public class Modulo{
        public List<Image> imagen = new List<Image>();
    }

    #region Variables
    
    public PlayerStats pStats;
    public Modulo vida = new Modulo();
    public Image armor;
    public string VariableFillAmountID = "Vector1_78A06364";

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < vida.imagen.Count; i++)
        {
            vida.imagen[i].material = Material.Instantiate(vida.imagen[i].material);
        }
        armor.material = Instantiate(armor.material);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateArmor();
    }

    void UpdateHealth(){
        if (vida.imagen.Count != pStats.health.nodos.Count) return;
        for(int i = 0; i < vida.imagen.Count; i++){
            vida.imagen[i].material.SetFloat(VariableFillAmountID, pStats.health.nodos[i].GetPercent);
        }
    }

    void UpdateArmor(){
        if (armor == null) return;
        armor.material.SetFloat(VariableFillAmountID, pStats.armor.GetPercent);
    }
}
