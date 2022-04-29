using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AIEnemy : MonoBehaviour
{
    //AI working
    public AIPath aiPath;
    public AIDestinationSetter destinationSetter;
    
    //State Attack - Enemy Stats
    public EnemyState enemyState;
    public SkillWeapon weapon;
    private void OnEnable()
    {
        //set ai parameter
        destinationSetter.target = GameObject.Find("Player").transform;
        aiPath.maxSpeed = enemyState.stats.speed;
        aiPath.endReachedDistance = enemyState.stats.rangeDetect;

        //get weapon
        Transform looper1 = gameObject.transform.Find("SlotWeapon");
        if (enemyState.stats.isMelee)
            weapon = looper1.transform.Find("Blade").GetComponent<SkillWeapon>();
        else
            weapon = looper1.transform.Find("Gun").GetComponent<SkillWeapon>();
    }

    private void Update()
    {
        if(aiPath.reachedEndOfPath)
        {
            //perfome skill if have
            //else
            Debug.Log("Hello");
            weapon.DetectRange();
        }
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, enemyState.stats.rangeDetect);
    }
}
