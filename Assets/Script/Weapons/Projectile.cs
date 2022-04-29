using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public WeaponStats stats;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == this.tag && this.tag == "Enemy")
        {
            
            collision.gameObject.GetComponent<EnemyState>().TakeDmg();
            Destroy(this.gameObject);
        }
            
        else if (collision.gameObject.tag == "Boundary")
            Destroy(this.gameObject);
    }
}
