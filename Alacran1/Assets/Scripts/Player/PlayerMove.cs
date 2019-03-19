using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    #region Variables
    protected CharacterController characterController;
    public Config config = new Config();
    [System.Serializable]
    public class Config
    {
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
        float Up = characterController.velocity.y;
        Debug.Log(Up);
        Debug.Log("fixedUpdatetime: "+ Time.fixedDeltaTime);
        if (!characterController.isGrounded) Up += (Physics.gravity.y * Time.fixedDeltaTime);
        else if (PlayerInput.current.isJump) Up = Up + config.JumpVel;
        Vector3 vel = new Vector3(PlayerInput.current.GetWalk().x, Up, PlayerInput.current.GetWalk().y);
        characterController.Move(vel);
    }
    #endregion

}
