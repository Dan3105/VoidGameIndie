using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AIEnemy : Characteristic
{
    //AI working
    public AIPath aiPath;
    public AIDestinationSetter destinationSetter;

    //State Attack - Enemy Stats
    public Transform slotWeapon;
    public SkillWeapon weapon;


    protected override void Start()
    {
        base.Start();
        //set ai parameter
        destinationSetter.target = GameObject.Find("Player").transform;
        aiPath.maxSpeed = stats.speed;
        if(weapon.isMelee)
            aiPath.endReachedDistance = weapon.stats.rangeDetect * 2;
        else
            aiPath.endReachedDistance = weapon.stats.rangeDetect * 2 / 3;


        //set stats to enemy
        crHp = stats.hp + 2f * GameManager.Instance.currentLevel;

        //Instantiate weapon
        var temp = Instantiate(weapon, slotWeapon);
        weapon = temp;

        //update stats for weapon
        weapon.stats.UpdateStats(GameManager.Instance.currentLevel);

        //set isAlive to true
        
    }

    private void FixedUpdate()
    {
        //cooldown attack for weapon
        weapon.cdCall -= Time.deltaTime;
        weapon.DetectRange();

    }

}
