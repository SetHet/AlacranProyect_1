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
        public KeyCode run = KeyCode.LeftShift;
        [Header("Vision")]
        public string LookRight = "Mouse X";
        public string LookUp = "Mouse Y";
        [Header("Acciones")]
        public string LeftClick = "Fire1";
        public KeyCode getItem = KeyCode.E;
        [Header("Items")]
        public KeyCode selectItemPistol = KeyCode.Alpha1;
        public KeyCode selectItemAmetralladora = KeyCode.Alpha2;
        public KeyCode selectItemShotgun = KeyCode.Alpha3;
        public KeyCode selectItemBotiquin = KeyCode.Alpha4;
        public KeyCode selectItemPlaca = KeyCode.Alpha5;

    }
    #endregion

    #region Methods

    public Vector2 GetWalk { get { return new Vector2(Input.GetAxis(nameInputs.MoverLateral), Input.GetAxis(nameInputs.MoverFrontal)); } }
    public bool isJump { get { return Input.GetButtonDown(nameInputs.Jump); } }
    public bool isRun { get { return Input.GetKey(nameInputs.run); } }


    public Vector2 GetLook()
    {
        return new Vector2(Input.GetAxis(nameInputs.LookRight), -Input.GetAxis(nameInputs.LookUp));
    }

    public bool GetItem()
    {
        return Input.GetKeyDown(nameInputs.getItem);
    }

    public bool SelectItemPistol()
    {
        return Input.GetKeyDown(nameInputs.selectItemPistol);
    }
    public bool SelectItemAmetralladora()
    {
        return Input.GetKeyDown(nameInputs.selectItemAmetralladora);
    }
    public bool SelectItemShotgun()
    {
        return Input.GetKeyDown(nameInputs.selectItemShotgun);
    }
    public bool SelectItemBotiquin()
    {
        return Input.GetKeyDown(nameInputs.selectItemBotiquin);
    }
    public bool SelectItemPlaca()
    {
        return Input.GetKeyDown(nameInputs.selectItemPlaca);
    }
    #endregion
}
