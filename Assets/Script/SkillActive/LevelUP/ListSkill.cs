using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSkill : MonoBehaviour
{
    public LayerMask whoIsEnemy;
    public float dmg;
    public float coolDown;
    public virtual void SkillActive() { }
}
