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
    public Modulo armor = new Modulo();

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < vida.imagen.Count; i++)
        {
            vida.imagen[i].material = Material.Instantiate(vida.imagen[i].material);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateArmor();
    }

    void UpdateHealth(){
        if (vida.imagen.Count != pStats._health.nodos.Count) return;
        for(int i = 0; i < vida.imagen.Count; i++){
            //vida.imagen[i].fillAmount = pStats._health.nodos[i].GetPercent;
            vida.imagen[i].material.SetFloat("Vector1_78A06364", pStats._health.nodos[i].GetPercent);
        }
    }

    void UpdateArmor(){

    }
}
