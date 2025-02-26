﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

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
            public float aceleracionMult = 1.5f;
            public float velocidadUP = 10f;
            public float desaceleracion = 20f;
            public float aceleracion = 20f;
            public float velocidadMaxima = 5f;
            [Header("Controles")]
            public string AxisHorizontal = "Horizontal";
            public string AxisVertical = "Vertical";
            public string ButtonJump = "Jump";
            [Header("Jump")]
            public float jumpCooldown = 0.3f;
            public float jumpVelocity = 5f;
            [Header("Gravity")]
            public Utilities.UtilitiesMath.AlgoritmoForPoint VersusGravedad = new UtilitiesMath.AlgoritmoForPoint();
        }

        private bool ActionFrame = false;
        private Vector2 DPad = Vector2.zero;
        private bool jump = false;
        private float jumpCoolDown = 0;

        private bool isGrounded = false;
        #endregion

        #region Basic Methods
        private void FixedUpdate()
        {
            DetectGround();
            if (isGrounded)
            {
                float horizontal = Input.GetAxis(config.AxisHorizontal);
                float vertical = Input.GetAxis(config.AxisVertical);
                bool jump = Input.GetButtonDown(config.ButtonJump);
                SetDPad(horizontal, vertical);
                if (jump) CallJump();

                Walk();
                Jump();
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
            DPad.Normalize();
        }

        public void CallJump()
        {
            jump = true;
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

                float dif = ((distanceDetect - hit.distance)/distanceDetect);
                config.rigid.AddForce(-Physics.gravity * (config.VersusGravedad.GetValue(dif)), ForceMode.Acceleration);
                Debug.Log("dif:" + dif + "__func:" + config.VersusGravedad.GetValue(dif) + "__Aceleration:"+ (-Physics.gravity * config.VersusGravedad.GetValue(dif)));
            }
            else
            {

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
                /*if (vel.magnitude <= config.desaceleracion * Time.deltaTime)
                {
                    config.rigid.AddForce(-vel, ForceMode.VelocityChange);
                }
                else
                {
                    config.rigid.AddForce(-vel.normalized * config.desaceleracion * Time.deltaTime, ForceMode.VelocityChange);
                }*/
                if (config.rigid.velocity.magnitude <= config.desaceleracion * Time.fixedDeltaTime)
                {
                    config.rigid.AddForce(-config.rigid.velocity, ForceMode.VelocityChange);
                }
                else
                {
                    config.rigid.AddForce(-config.rigid.velocity.normalized * config.desaceleracion * Time.fixedDeltaTime, ForceMode.VelocityChange);
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

        #region Jump
        void Jump()
        {
            if (jump)
            {
                jump = false;
                if (jumpCoolDown > Time.time) return;
                jumpCoolDown = Time.time + config.jumpCooldown;

                config.rigid.AddForce(config.jumpVelocity * Vector3.up, ForceMode.VelocityChange);
            }
        }
        #endregion


    }
}