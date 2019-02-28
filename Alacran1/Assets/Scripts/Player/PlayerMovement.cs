using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {

        #region variables
        public Configuracion config = new Configuracion();
        [System.Serializable]
        public class Configuracion
        {
            public float height = 1.8f;

            public CapsuleCollider collider;
            public float distanceDetectRepair;
            public float DDR { get { return distanceDetectRepair; } }

        }

        private bool ActionFrame = false;
        private Vector2 DPad = Vector2.zero;
        private bool Jump = false;
        #endregion

        #region Basic Methods
        private void Update()
        {
            if (ActionFrame)
            {
                ActionFrame = false;
            }
        }
        #endregion

        #region Input
        public void InputComplete()
        {
            ActionFrame = true;
        }

        public void SetDPad(float x = 0f, float y = 0f)
        {
            DPad.x = x;
            DPad.y = y;
        }

        public void CallJump()
        {
            Jump = true;
        }
        #endregion

        #region Ground

        #endregion



    }
}