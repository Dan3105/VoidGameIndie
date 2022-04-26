using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Object Data", fileName = "Object Data")]
public class CharacterStats : ScriptableObject
{
    public float hp;

    public float speed;

    public float rangeDetect;

    public bool isMelee;
    
}
