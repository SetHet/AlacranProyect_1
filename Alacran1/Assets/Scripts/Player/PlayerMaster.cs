using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMaster : MonoBehaviour
    {
        #region Singleton
        private static PlayerMaster _instance;
        public static PlayerMaster instance { get { return _instance; } }

        void InitSingleton()
        {
            if (_instance != null)
            {
                Debug.Log("Hay más de un PlayerMaster. " + name);
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }
        #endregion

        #region Basic Methods
        private void Awake()
        {
            InitSingleton();
        }
        #endregion
    }
}