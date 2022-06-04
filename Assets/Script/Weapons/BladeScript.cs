using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BladeScript : SkillWeapon
{
    //scriptable blade
    public Vector2 size;
    public Vector2 sizePos;
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
        var temp = SoundManager.Instance.sounds["Slash"];

        SoundManager.Instance.source.PlayOneShot(temp);

        Vector2 position = sizePos + (Vector2)transform.position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(position, size, 0, whoWasAttacked);
        
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            //enemy take damamage;
            //Debug.Log(collider2Ds[i].name + "Take dmg");
            collider2Ds[i].gameObject.GetComponent<Characteristic>().TakeDmg(stats.dmgAtk);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 position = (Vector2)transform.position + sizePos;
        //range Detecting
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.rangeDetect);
        //range attack
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position, size);
    }
}
