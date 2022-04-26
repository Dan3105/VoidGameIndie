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
        GameObject pj = Instantiate(projectile, transform.position, transform.rotation);
        pj.GetComponent<Rigidbody2D>().AddForce(dir * 15, ForceMode2D.Impulse);
       // Debug.Log("shooot");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//range
        Gizmos.DrawWireSphere(transform.position, stats.rangeDetect);
    }
}
