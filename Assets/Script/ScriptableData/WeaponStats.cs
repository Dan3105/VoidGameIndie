using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Data", fileName = "Create Weapon Stats")]
public class WeaponStats : ScriptableObject {
    public float dmgAtk;
    public float cdAtk;
    public float rangeDetect;

    public float currentDmg;
    public float currentCD;

    public void UpdateStats(int level)
    {
        currentDmg = dmgAtk * 1.5f * level;
        currentCD = cdAtk - 0.015f * level;
    }
}
