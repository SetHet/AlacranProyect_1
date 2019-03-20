using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    #region Variables
    protected Vector3 velocity = Vector3.zero;
    protected CharacterController characterController;
    public Config config = new Config();
    [System.Serializable]
    public class Config
    {
        public float VelocidadWalk = 3f;
        public float VelocidadRun = 6f;
        public float VelocidadRunBoorst = 10f;
        public float JumpVel = 5f;
    }
    #endregion

    #region Basic Methods
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        //Calcular Caida y Salto 
        float Up = 0;
        if (!characterController.isGrounded) Up = velocity.y;
        Up += (Physics.gravity.y * Time.fixedDeltaTime);
        if (PlayerInput.current.isJump && characterController.isGrounded) Up = Up + config.JumpVel;

        //Calcular Velocidad y guardar esta
        Vector3 vel = new Vector3();
        vel += characterController.transform.forward * PlayerInput.current.GetWalk().y;
        vel += characterController.transform.right * PlayerInput.current.GetWalk().x;
        vel = vel.normalized * config.VelocidadWalk;
        vel.y += Up;
        characterController.Move(vel * Time.fixedDeltaTime);
        velocity = vel;
    }
    #endregion

}
