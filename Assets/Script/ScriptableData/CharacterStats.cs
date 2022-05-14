using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(menuName = "Create Object Data", fileName = "Object Data")]
public class CharacterStats : ScriptableObject
{
    public float hp;
    public float speed;
    public float def;
    public float exp;

    public float crHp;
    public float crSpeed;
    public float crDef;
    public float crExp;

    public RuntimeAnimatorController animator;
    

    public void UpdateStats(int level)
    {
        crHp = ((1 + level * 2) / 3) * hp;
        crHp = ((1 + level * 2) / 3) * hp;
        crHp = ((1 + level * 2) / 3) * hp;
        crHp = ((1 + level * 2) / 3) * hp;
    }
}
