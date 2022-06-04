using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezing : ListSkill
{
    public float range;
    public float cdFreeze;
    public GameObject vfxEffect;
    private Transform playerPos;
    private float crCD = 0;
    
    public override void SkillActive()
    {
        Instantiate(this.gameObject);
        
    }

    private void FixedUpdate()
    {
        if (crCD <= 0)
        {
            playerPos = GameManager.Instance.playerStats.transform;
            GameObject effect = Instantiate(vfxEffect.gameObject, playerPos.position, Quaternion.identity);
            effect.transform.parent = this.transform;
            Collider2D[] enemyRange = Physics2D.OverlapCircleAll(playerPos.position, range, whoIsEnemy);
            dmg = GameManager.Instance.playerStats.weapon.stats.currentDmg * 1.1f;
            foreach (Collider2D enemy in enemyRange)
            {
                AIEnemy refEnemy = enemy.GetComponent<AIEnemy>();
                StartCoroutine(SlowDown(refEnemy));
            }
            crCD = Random.Range(coolDown, coolDown + 2);
        }
        else
            crCD -= Time.deltaTime;
    }


    public IEnumerator SlowDown(AIEnemy enemy)
    {
        enemy.TakeDmg(dmg);
        enemy.aiPath.canMove = false;

        yield return new WaitForSeconds(cdFreeze);
        
        enemy.aiPath.canMove = true;

    }


}