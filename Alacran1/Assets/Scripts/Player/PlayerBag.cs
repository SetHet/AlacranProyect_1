using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    public Items items;
    [System.Serializable]
    public class Items
    {
        [SerializeField] protected short _botoquines = 0;
        [SerializeField] protected short _max_botiquines = 3;
        public short botiquines { get { return _botoquines; } }
        public short maxBotiquines { get { return _max_botiquines; } }
        public ushort AddBotiquin(ushort cantidad)
        {
            

            return 0;
        }


        [SerializeField] protected int _placasArmadura = 0;

    }
}
