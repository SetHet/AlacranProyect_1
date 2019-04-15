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
        CalcularVelocidad(ref vel);
        vel.y += Up;
        characterController.Move(vel * Time.fixedDeltaTime);
        velocity = vel;
    }
    #endregion

    #region Methods
    void CalcularVelocidad(ref Vector3 vel)
    {
        vel += characterController.transform.forward * PlayerInput.current.GetWalk.y;
        vel += characterController.transform.right * PlayerInput.current.GetWalk.x;
        vel = vel.normalized;

        if (PlayerInput.current.isRun)
        {
            if (PlayerStats.current.Energy_Use(Time.fixedDeltaTime))
                vel *= Utilities.UtilitiesMath.RemapFloat(PlayerStats.current.energy.percent, 0f, 1f, config.VelocidadRun, config.VelocidadRunBoorst);
            else
                vel *= config.VelocidadRun;
        }
        else
        {
            vel *= config.VelocidadWalk;
        }
    }
    #endregion
}
