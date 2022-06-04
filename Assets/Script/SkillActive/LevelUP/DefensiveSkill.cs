using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefensiveSkill")]
public class DefensiveSkill : AbilitiesStats
{
    public enum TYPE_INCREASE
    {
        HP_PLUS,
        DMG_PLUS,
        SPEED_PLUS,
        ATK_SPD
    }
    public override TYPE_ABILITY type => TYPE_ABILITY.DEFENSIVE; 
    public TYPE_INCREASE typeIncrease;
    public float percent;
    public override void SkillActive()
    {

        switch (typeIncrease)
        {
            case DefensiveSkill.TYPE_INCREASE.HP_PLUS:
                GameManager.Instance.playerStats.stats.UpgradeHp(percent);
                break;
            case DefensiveSkill.TYPE_INCREASE.DMG_PLUS:
                GameManager.Instance.weapon.stats.SetDmg(percent);
                break;
            case DefensiveSkill.TYPE_INCREASE.SPEED_PLUS:
                GameManager.Instance.playerStats.stats.UpgradeSpeed(percent);
                break;
            case DefensiveSkill.TYPE_INCREASE.ATK_SPD:
                GameManager.Instance.weapon.stats.SetCD(percent);
                break;

        }

    }
}
