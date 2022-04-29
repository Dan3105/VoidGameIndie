using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BladeScript : SkillWeapon
{
    //scriptable blade
    public Vector2 size;
    public Collider2D col2D;
    
    protected override void Start()
    {
        base.Start();
    }

    public override void DetectRange()
    {
        base.DetectRange();
    }

    public override void Attacking()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, size, 0, whoWasAttacked);
        for(int i = 0; i < collider2Ds.Length; i++)
        {
            // enemy take damamage;
            collider2Ds[i].gameObject.GetComponent<Characteristic>().TakeDmg();
            Debug.Log("Hello");
        }
    }

    
    private void OnDrawGizmos()
    {
        //range Detecting
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.rangeDetect);
        //range attack
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
