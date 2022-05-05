using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public WeaponStats stats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == this.tag && collision.gameObject.tag == "Enemy")
        {

            collision.gameObject.GetComponent<AIEnemy>().TakeDmg(stats.dmgAtk);
            
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == this.tag && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController>().TakeDmg(stats.dmgAtk);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Boundary")
            Destroy(this.gameObject);
            
    }

}
