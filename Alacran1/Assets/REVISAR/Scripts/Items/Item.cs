using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Enum
    public enum Tipo
    {
        Default,
        Suministro,
        Ammo
    }

    public enum Tipo_Suministro
    {
        None, 
        Botiquin,
        PlacaArmadura
    }

    public enum Tipo_Ammo
    {
        None,
        Shotgun,
        Pistol,
        Ametralladora
    }
    #endregion

    #region Variables
    public Tipo tipo = Tipo.Default;
    public Tipo_Suministro suministro = Tipo_Suministro.None;
    public Tipo_Ammo ammo = Tipo_Ammo.None;

    public ushort cantidad = 0;
    #endregion

    #region BasicMethods
    
    #endregion
}
