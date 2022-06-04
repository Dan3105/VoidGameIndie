using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : SkillWeapon
{

    public Projectile projectile;
    override protected void Start()
    {
        base.Start();
        projectile.stats = stats;
    }

    public override void DetectRange()
    {
        base.DetectRange();
    }

    public override void Attacking()
    {
        var temp = SoundManager.Instance.sounds["Projectile"];

        SoundManager.Instance.source.PlayOneShot(temp);

        var pj = Instantiate(projectile, transform.position, transform.rotation);
        
        int tagName = (int)Mathf.Log(whoWasAttacked, 2);
        
        pj.tag = LayerMask.LayerToName(tagName);
        pj.stats = stats;
        pj.rg2d.AddForce(dir * 15, ForceMode2D.Impulse);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//range
        Gizmos.DrawWireSphere(transform.position, stats.rangeDetect);
    }
}
