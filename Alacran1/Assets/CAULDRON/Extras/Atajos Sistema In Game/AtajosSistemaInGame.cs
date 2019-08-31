using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extra
{
    public class AtajosSistemaInGame : MonoBehaviour
    {

        #region Vars
        public Configuracion config;
        [System.Serializable] public class Configuracion
        {
            [Header("Interfaces")]
            public GameObject main;
            public GameObject rendimiento;

            [Header("Controles")]
            public KeyCode button_general = KeyCode.RightControl;
            public KeyCode button_rendimiento = KeyCode.U;
            public KeyCode button_mouseLock = KeyCode.L;
        }

        public Estado state;
        [System.Serializable] public class Estado
        {
            public bool enable_rendimiento = false;
        }
        #endregion

        #region Comportamiento
        private void Start()
        {
            DisableAll();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (Input.GetKey(config.button_general))
            {
                //Preparacion
                DisableAll();
                SetActiveInterfaz(config.main, true, "Interfaz principal (main)");
                // Especiales
                if (Input.GetKeyDown(config.button_rendimiento)) state.enable_rendimiento = !state.enable_rendimiento;
                if (Input.GetKeyDown(config.button_mouseLock))
                {
                    if (Cursor.lockState == CursorLockMode.None)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
            }
            else
            {
                SetActiveInterfaz(config.main, false, "Interfaz principal (main)");
                if (state.enable_rendimiento) SetActiveInterfaz(config.rendimiento, true, "Interfaz de rendimiento");
            }
        }

        private void DisableAll()
        {
            SetActiveInterfaz(config.main, false, "Interfaz Principal (main)");
            SetActiveInterfaz(config.rendimiento, false, "Interfaz de rendimiento");
        }

        private void SetActiveInterfaz(GameObject obj, bool value, string nameObj = "")
        {
            if (obj == null)
            {
                Warning("SetActiveInterfaz no se le entrego alguno de los objetos.\n>>" + nameObj + "\n");
                return;
            }

            obj.SetActive(value);
        }

        private void Warning(string mensaje)
        {
            Debug.LogWarning("Warning Atajos Sistema:\n" + mensaje);
        }
        #endregion


    }
}