using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public Configuration config;
        [System.Serializable]
        public class Configuration
        {
            public Transform body;
            public Camera cam;
            public CharacterController characteterController;
            public float normalY = 0.3f;
            public float distanceWalk = 0.7f;
            public float alturaWalk = 0.2f;
            public float angleMax = 90f;
            public float sensibilidad = 1f;
        }

        public bool disableHeadLoop = false;

        private float auxDist = 0f;

        #region Basic Methods
        private void Update()
        {
            Vector2 LookVelocity = PlayerInput.current.GetLook();
            Look(LookVelocity.x, LookVelocity.y);
            HeadLoop();
        }
        #endregion

        #region Look
        public void Look(float x, float y)
        {
            config.body.Rotate(Vector3.up, x * config.sensibilidad, Space.Self);
            config.cam.transform.Rotate(Vector3.right, y * config.sensibilidad, Space.Self);
            float aux_angle = config.cam.transform.localRotation.eulerAngles.x;
            if (aux_angle > 45 && aux_angle <= 180)
            {
                config.cam.transform.localRotation = Quaternion.AngleAxis(45, Vector3.right);
            }
            else if(aux_angle < 315 && aux_angle > 180)
            {
                config.cam.transform.localRotation = Quaternion.AngleAxis(45, -Vector3.right);
            }
        }
        #endregion

        #region HeadLoop
        void HeadLoop()
        {
            if (disableHeadLoop) return;
            float altura = config.normalY;

            auxDist += config.characteterController.velocity.magnitude * Time.deltaTime;
            auxDist %= config.distanceWalk;

            //Debug.Log("Velocity: " + config.rigid.velocity);

            altura += UtilitiesMath.RemapFloat(UtilitiesMath.SenAbs2(auxDist, config.distanceWalk), 0, 1, 0, config.alturaWalk);

            config.cam.transform.localPosition = altura * Vector3.up; 
        }
        #endregion
    }
}