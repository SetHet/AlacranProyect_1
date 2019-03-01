using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        [Header("Componentes")]
        public PlayerMovement playerMovement;
        public PlayerCamera playerCamera;
        [Header("Controles")]
        public string AxisHorizontal = "Horizontal";
        public string AxisVertical = "Vertical";
        public string ButtonJump = "Jump";
        public string AxisLookHorizontal = "Mouse X";
        public string AxisLookVertical = "Mouse Y";


        private void Update()
        {
            float horizontal = Input.GetAxis(AxisHorizontal);
            float vertical = Input.GetAxis(AxisVertical);
            bool jump = Input.GetButtonDown(ButtonJump);

            float LookHorizontal = Input.GetAxis(AxisLookHorizontal);
            float LookVertical = -Input.GetAxis(AxisLookVertical);

            if (playerMovement != null)
            {
                playerMovement.SetDPad(horizontal, vertical);
                if (jump) playerMovement.CallJump();

                playerMovement.InputComplete();
            }

            if (playerCamera != null)
            {
                playerCamera.Look(LookHorizontal, LookVertical);
            }
        }
    }
}