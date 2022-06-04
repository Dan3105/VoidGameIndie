using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchProjectile : ListSkill
{
    public Projectile typeProjectile;
    public override void SkillActive()
    {
        GunScript refGun = GameManager.Instance.weapon as GunScript; 
        refGun.projectile = typeProjectile;
    }
}
