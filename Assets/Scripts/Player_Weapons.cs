using UnityEngine;
using System.Collections;

[System.Serializable]
public class Player_Weapons
{
    public string weapon_Name = "String";
    public float weapon_Range = 100;
    public float weapon_Accuracy = 1;
    public int weapon_RayCastsPerShot = 1;
    [Space(5)]
    public bool weapon_Automatic = false;
    public float weapon_ShotsPerSecond = 0.5f;
    [Space(5)]
    public int weapon_AmmoCurrent = 100;
    public int weapon_AmmoMax = 100;
    [Space(5)]
    public float weapon_MinDamage = 100;
    public float weapon_MaxDamage = 100;
}
