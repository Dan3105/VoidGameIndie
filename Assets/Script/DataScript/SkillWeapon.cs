using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SkillWeapon : MonoBehaviour
{
    //scriptable weapon
    public WeaponStats stats;

    //
    protected GameObject slotFather;
    public LayerMask whoWasAttacked;

    protected Vector2 dir;
    private float cdCall;
    protected virtual void Start()
    {
        slotFather = this.transform.parent.gameObject;
        cdCall = 0;
    }
    public virtual void DetectRange()
    {
        cdCall -= Time.deltaTime;

            Collider2D[] hits = Physics2D.OverlapCircleAll(slotFather.transform.position, stats.rangeDetect, whoWasAttacked);
        if (hits.Length > 0)
        {
            Collider2D nearest = hits[0];
            for (int i = 1; i < hits.Length; i++)
            {
                float currentDis = Vector2.Distance(nearest.transform.position, slotFather.transform.position);
                float newDis = Vector2.Distance(hits[i].transform.position, slotFather.transform.position);
                if (currentDis > newDis)
                    nearest = hits[i];
            }

            dir = (nearest.transform.position - slotFather.transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            slotFather.transform.localRotation = Quaternion.Euler(0, 0, angle);
            if (cdCall < 0)
            {
                Attacking();
                cdCall = stats.cdAtk;
            }
        }
        
    }
    public virtual void Attacking() { }


}
