using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWhirlWind : MonoBehaviour
{
    public LayerMask whoIsEnemy;
    public float timeDuration;
    public float dmg;
    public float speed;
    [HideInInspector] public float range;
    [HideInInspector] public Vector3 startPos;
    public float startAngle;
    private float cdTime;

    private void Update()
    {

        UpdatePath();

        //Calculate diretion
        startAngle = (startAngle + Time.deltaTime * speed * 10) % 360;

        cdTime = (cdTime + Time.deltaTime);
    }

    public void UpdatePath()
    {
        float x = transform.parent.position.x + Mathf.Cos(startAngle * Mathf.Deg2Rad) * range;
        float y = transform.parent.position.y + Mathf.Sin(startAngle * Mathf.Deg2Rad) * range;
        
        Vector3 temp = new Vector3(x, y, 0);

        this.transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)Mathf.Log(whoIsEnemy, 2))
        {
            collision.gameObject.GetComponent<AIEnemy>().TakeDmg(dmg);
            cdTime = timeDuration;
        }
    }

    public void SetStats(float dmg, float timeDuration)
    {
        this.dmg = dmg;
        this.timeDuration = timeDuration;
    }
}
