using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    #region Vars
    public Health _health = new Health();
    public Armor armor = new Armor();
    #endregion

    #region Classes
    [System.Serializable]
    public class Health{
        
        #region Class
        [System.Serializable]
        public class Node{
            [SerializeField]float max = 25;
            float current = 0;

            public void Init(){
                current = max;
            }

            public float Heal(float value){
                current += value;
                if (current > max){
                    float sobrante = current - max;
                    current = max;
                    return sobrante;
                }
                else{
                    return 0f;
                }
            }

            public float Damage(float value){
                current -= value;
                if (current < 0){
                    float sobrante = -current;
                    current = 0f;
                    return sobrante;
                }
                else{
                    return 0f;
                }
            }

            public bool isDisable { get { return current <= 0f; } }
            public bool isComplete { get { return current >= max; }}
            public float GetCurrent { get { return current; } }
            public float GetMax { get {return max; } }
            public float GetPercent {get { return current / max; } }
        }
        #endregion

        #region Vars
        public List<Node> nodos = new List<Node>();
        public float CooldownRegenerate = 5f;
        public float RegenerationPerSecond = 10f;
        float ContinueRegeneration = 0f;
        #endregion

        #region Methods
        public void Init(){
            foreach (var item in nodos)
            {
                item.Init();
            }
        }

        public void AutoRegeration(float deltaTime){
            if (ContinueRegeneration > Time.time) return;
            float value = deltaTime * RegenerationPerSecond;
            for (int i = 0; i < nodos.Count; i++)
            {
                if (nodos[i].isComplete) continue;
                if (nodos[i].isDisable) break;
                value = nodos[i].Heal(value);
                if (value == 0) break;
            }
            return;
        }

        public void AddDamage(float value){
            ContinueRegeneration = Time.time + CooldownRegenerate;
            for (int i = nodos.Count - 1; i >=0; i--){
                if (nodos[i].isDisable) continue;
                value = nodos[i].Damage(value);
            }

            if (nodos.Count > 0 && nodos[0].isDisable) Debug.Log("Death player");
        }

        public void DebugLifeTopZero(){
            DebugLookCurrent();
            AddDamage(99);
            DebugLookCurrent();
        }

        public void DebugLookCurrent(){
            string mensaje = "HealthCurrent >>>> ";
            for(int i = 0; i < nodos.Count; i++){
                mensaje += "num:"+i+": Valor:"+nodos[i].GetCurrent+">>";
            }
            Debug.Log(mensaje);
        }
        #endregion
    }

    [System.Serializable]
    public class Armor{

    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _health.Init();
    }

    // Update is called once per frame
    void Update()
    {
        _health.AutoRegeration(Time.deltaTime);
    }

    public void D(){
        _health.DebugLifeTopZero();
    }
}
