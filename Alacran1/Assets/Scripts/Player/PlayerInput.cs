using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Basic Methods
    private void Awake()
    {
        Singleton();
    }
    #endregion

    #region Singleton
    protected static PlayerInput _current;
    public static PlayerInput current { get { return _current; } }
    public void Singleton()
    {
        if (_current != null)
        {
            Debug.LogWarning("Hay 2 Input Player");
            Destroy(gameObject);
            return;
        }

        _current = this;
    }
    #endregion

    #region Variables
    public NameInputs nameInputs = new NameInputs();
    [System.Serializable]
    public class NameInputs
    {
        [Header("Movimiento")]
        public string MoverLateral = "Horizontal";
        public string MoverFrontal = "Vertical";
        public string Jump = "Jump";
        [Header("Vision")]
        public string LookRight = "Mouse X";
        public string LookUp = "Mouse Y";

    }
    #endregion

    #region Methods
    public Vector2 GetWalk()
    {
        return new Vector2(Input.GetAxis(nameInputs.MoverLateral), Input.GetAxis(nameInputs.MoverFrontal));
    }

    public bool isJump
    {
        get
        {
            return Input.GetButtonDown(nameInputs.Jump);
        }
    }

    public Vector2 GetLook()
    {
        return new Vector2(Input.GetAxis(nameInputs.LookRight), -Input.GetAxis(nameInputs.LookUp));
    }
    #endregion
}
