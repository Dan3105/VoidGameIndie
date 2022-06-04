using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile
{
    public LayerMask whoIsEnemy;
    public GameObject vfxBomb;
    public float range;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, range, whoIsEnemy);
            Instantiate(vfxBomb, transform.position, Quaternion.identity);
            foreach(var collider in collider2Ds)
            {
                float plusDmg = stats.currentDmg * .95f;
                collider.GetComponent<AIEnemy>().TakeDmg(plusDmg);
            }
            Destroy(this.gameObject);
        }
    }
}
