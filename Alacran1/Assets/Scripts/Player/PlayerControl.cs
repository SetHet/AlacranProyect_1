using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        [Header("Componentes")]
        public PlayerMovement playerMovement;
        [Header("Controles")]
        public string AxisHorizontal = "Horizontal";
        public string AxisVertical = "Vertical";
        public string ButtonJump = "Jump";


        private void Update()
        {
            float horizontal = Input.GetAxis(AxisHorizontal);
            float vertical = Input.GetAxis(AxisVertical);
            bool jump = Input.GetButtonDown(ButtonJump);

            if (playerMovement != null)
            {
                playerMovement.SetDPad(horizontal, vertical);
                if (jump) playerMovement.CallJump();

                playerMovement.InputComplete();
            }

        }
    }
}