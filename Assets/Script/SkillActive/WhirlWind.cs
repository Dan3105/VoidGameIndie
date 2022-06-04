using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlWind : ListSkill
{
    public SubWhirlWind[] whirldList = new SubWhirlWind[3];
    public float range;
    
    public override void SkillActive()
    {
        Transform playerTransform = GameManager.Instance.playerStats.transform;
        var thisSkill = Instantiate(this.gameObject, playerTransform.position, Quaternion.identity);
        thisSkill.transform.parent = playerTransform;
        float startAngel = 0f;
        foreach(var whirl in whirldList)
        {
            whirl.whoIsEnemy = whoIsEnemy;
            whirl.range = range;
            whirl.startAngle = startAngel;
            whirl.dmg = dmg;
            startAngel += 120;
            whirl.UpdatePath();
            whirl.gameObject.SetActive(true);
          
        }
       // Debug.Break();
    }

}
