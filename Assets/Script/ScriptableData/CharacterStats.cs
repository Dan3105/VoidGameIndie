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
    

    public void UpdateStats(int level)
    {
        crHp = ((1 + level * 2) / 3) * hp;
        crSpeed = 0.04f * Mathf.Pow(level, 3) + 0.1f * Mathf.Pow(level, 2) + speed;
        crExp = (crExp + 50) * level * (level - 1) ;
    }
}
