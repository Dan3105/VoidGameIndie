using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SkillWeapon : MonoBehaviour
{
    //scriptable weapon
    public WeaponStats stats;

    public bool isMelee;
    //
    public LayerMask whoWasAttacked;

    protected Vector2 dir;
    public float cdCall;

    protected virtual void Start()
    {
        cdCall = 0;
    }
    public virtual void DetectRange()
    { 
        if (transform.parent != null)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.parent.transform.position, stats.rangeDetect, whoWasAttacked);
            if (hits.Length > 0)
            {
                
                Collider2D nearest = hits[0];
                for (int i = 1; i < hits.Length; i++)
                {
                    float currentDis = Vector2.Distance(nearest.transform.position, transform.parent.transform.position);
                    float newDis = Vector2.Distance(hits[i].transform.position, transform.parent.transform.position);
                    if (currentDis > newDis)
                        nearest = hits[i];
                }

                dir = (nearest.transform.position - transform.parent.transform.position).normalized;

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                
                if (nearest.transform.position.normalized.x > this.transform.position.normalized.x) 
                {
                    Transform character = transform.parent.parent;
                    character.transform.localScale = new Vector2(Mathf.Abs(character.localScale.x), character.localScale.y);
                    transform.parent.transform.localRotation = Quaternion.Euler(0, 0, angle);
                }
                else if(nearest.transform.position.normalized.x < this.transform.position.normalized.x)
                {
                    Transform character = transform.parent.parent;
                    character.transform.localScale = new Vector2(-Mathf.Abs(character.localScale.x), character.localScale.y);
                    transform.parent.transform.localRotation = Quaternion.Euler(0, 0, -angle);
                }


               
                if (cdCall < 0)
                {
                    Attacking();
                    cdCall = stats.cdAtk;
                }
            }
        }
        
        
    }

    public virtual void Attacking() { }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, stats.rangeDetect);
    }
}
