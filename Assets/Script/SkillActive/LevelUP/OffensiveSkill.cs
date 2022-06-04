using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OffensiveSkill")]
public class OffensiveSkill : AbilitiesStats
{
    //public EnumSkillType.TYPE_SKILL typeSkill;
    public override TYPE_ABILITY type => TYPE_ABILITY.OFFENSIVE; 
    public LayerMask whoIsEnemy;
    public float dmg;
    public float coolDown;


    [SerializeField] ListSkill listSkill;
    public override void SkillActive()
    {

        listSkill.SkillActive();
    }
}
