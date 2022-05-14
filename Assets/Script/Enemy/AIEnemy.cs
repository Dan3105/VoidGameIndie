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

    [Header("Animation")]
    public Animator animatorEnemy;

    protected override void Start()
    {
        base.Start();
        //set ai parameter
        destinationSetter.target = GameManager.Instance.playerStats.transform;
        aiPath.maxSpeed = stats.speed;
        if(weapon.isMelee)
            aiPath.endReachedDistance = weapon.stats.rangeDetect;
        else
            aiPath.endReachedDistance = weapon.stats.rangeDetect * 2 / 3;


        //set stats to enemy
        crHp = stats.hp + 2f * GameManager.Instance.currentLevel;
        
        //update stats for weapon
        weapon.stats.UpdateStats(GameManager.Instance.currentLevel);

        animatorEnemy.runtimeAnimatorController = stats.animator;
        weapon.animator = animatorEnemy;
        Debug.Log(animatorEnemy.name + " Name: " + weapon.name);
    }

    private void Update()
    {
        animatorEnemy.SetFloat("Horizontal", weapon.dir.x);
        animatorEnemy.SetFloat("Vertical", weapon.dir.y);
        
    }

    private void FixedUpdate()
    {
        //cooldown attack for weapon
        weapon.cdCall -= Time.deltaTime;
        weapon.DetectRange();
        
    }

}
