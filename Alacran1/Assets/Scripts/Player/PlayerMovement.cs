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
            public Rigidbody rigid;
            public float distanceDetectRepair;
            public LayerMask groundLayer = -1;
            public float velocidadUP = 10f;
            public float desaceleracion = 20f;
            public float aceleracion = 20f;
            public float velocidadMaxima = 5f;
        }

        private bool ActionFrame = false;
        private Vector2 DPad = Vector2.zero;
        private bool Jump = false;

        private bool isGrounded = false;
        #endregion

        #region Basic Methods
        private void Update()
        {
            DetectGround();
            if (ActionFrame)
            {
                Walk();
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
        public void DetectGround()
        {
            isGrounded = false;

            Vector3 dirUP = Vector3.up;
            float radius = config.collider.radius - config.distanceDetectRepair;
            Vector3 startPoint = config.collider.transform.position + config.collider.center + dirUP*(-config.collider.height/2f) + dirUP*config.distanceDetectRepair + radius*dirUP;
            float distanceDetect = config.height + (-config.collider.height) + config.distanceDetectRepair*2 + radius;
            
            RaycastHit hit;
            if (Physics.SphereCast(startPoint, radius, -dirUP, out hit, distanceDetect, config.groundLayer, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;

                float dif = distanceDetect - hit.distance;
                if (dif < config.velocidadUP * Time.deltaTime)
                {
                    config.collider.transform.position += dirUP * dif;
                }
                else
                {
                    config.collider.transform.position += dirUP * config.velocidadUP * Time.deltaTime;
                }
                config.rigid.useGravity = false;
            }
            else
            {
                config.rigid.useGravity = true;
            }
        }
        #endregion

        #region Walk
        void Walk()
        {
            if (!isGrounded) return;

            Vector3 vel = config.rigid.velocity;
            vel.y = 0f;

            if (DPad == Vector2.zero)
            {
                if (vel.magnitude <= config.desaceleracion * Time.deltaTime)
                {
                    config.rigid.AddForce(-vel, ForceMode.VelocityChange);
                }
                else
                {
                    config.rigid.AddForce(-vel.normalized * config.desaceleracion * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            else
            {
                config.rigid.AddForce(config.rigid.transform.forward * config.aceleracion * DPad.y, ForceMode.Acceleration);
                config.rigid.AddForce(config.rigid.transform.right * config.aceleracion * DPad.x, ForceMode.Acceleration);
            }

            if (vel.magnitude > config.velocidadMaxima)
            {
                config.rigid.velocity = vel.normalized * config.velocidadMaxima + config.rigid.velocity.y * Vector3.up;
            }

        }
        #endregion

        
    }
}