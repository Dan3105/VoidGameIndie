using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : SkillWeapon
{

    public GameObject projectile;
    override protected void Start()
    {
        base.Start();
        projectile.GetComponent<Projectile>().stats = stats;
    }

    public override void DetectRange()
    {
        base.DetectRange();
    }

    public override void Attacking()
    {
        Debug.Log(this.transform.parent.parent.name + "attack");
        GameObject pj = Instantiate(projectile, transform.position, transform.rotation);
        
        int tagName = (int)Mathf.Log(whoWasAttacked, 2);
        
        pj.tag = LayerMask.LayerToName(tagName);
        pj.GetComponent<Projectile>().stats = stats;
        pj.GetComponent<Rigidbody2D>().AddForce(dir * 15, ForceMode2D.Impulse);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//range
        Gizmos.DrawWireSphere(transform.position, stats.rangeDetect);
    }
}
