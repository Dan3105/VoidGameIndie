using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(menuName = "Create Object Data", fileName = "Object Data")]
public class CharacterStats : ScriptableObject
{
    public float hp;
    public float speed;

    public float exp;

    public float crHp;
    public float crSpeed;

    public float crExp;

    public RuntimeAnimatorController animator;
    
    public void SetStats(bool isPlayer)
    {
        crHp = hp;
        if(!isPlayer)
            crSpeed = Random.Range(speed, speed - 2);
        crSpeed = speed;
        crExp = exp;
    }

    public void UpdateStats(int level)
    {
        crHp = ((1 + level * 2) / 3) * hp;
        
        crExp = 50 * level ;
    }

    public void UpgradeHp(float percent)
    {
        crHp += percent * crHp;
    }

    public void UpgradeSpeed(float percent)
    {
        crHp += percent * crHp;
    }


    public float percentBar(float runTime)
    {
        return runTime / crHp;
    }
}
