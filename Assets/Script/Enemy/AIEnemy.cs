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
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject boomEffect;
    Material enemyMaterial;
    protected override void Start()
    {
        base.Start();
        boomEffect.SetActive(false);
        enemyMaterial = spriteRenderer.material;
        enemyMaterial.SetFloat("_FadeValue", 1f);
        Debug.Log("<color = blue>" + enemyMaterial.GetFloat("_FadeValue"));
        //set ai parameter
        destinationSetter.target = GameManager.Instance.playerStats.transform;
        aiPath.maxSpeed = stats.speed;
        if(weapon.isMelee)
            aiPath.endReachedDistance = weapon.stats.rangeDetect;
        else
            aiPath.endReachedDistance = weapon.stats.rangeDetect * 2 / 3;

        
        //update stats for weapon
        weapon.stats.UpdateStats(GameManager.Instance.currentLevel);

        animatorEnemy.runtimeAnimatorController = stats.animator;
        weapon.animator = animatorEnemy;
        Debug.Log(animatorEnemy.name + " Name: " + weapon.name);
    }

    private void Update()
    {

        if(GameManager.Instance.playerStats.crHp >= 0 && crHp > 0)
        {
            Vector2 dir = destinationSetter.target.position - transform.position;
            animatorEnemy.SetFloat("Horizontal", dir.x);
            animatorEnemy.SetFloat("Vertical", dir.y);
        }

        if(crHp <= 0)
        {
            PlayDeath();
            if (enemyMaterial.GetFloat("_FadeValue") > 0)
                enemyMaterial.SetFloat("_FadeValue", enemyMaterial.GetFloat("_FadeValue") - Time.deltaTime);
            else
            {
                
                boomEffect.SetActive(true);
                StartCoroutine("Blooming");
            }
               
        }
        
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.playerStats.crHp > 0 && crHp > 0)
        {
            //cooldown attack for weapon
            weapon.cdCall -= Time.deltaTime;
            weapon.DetectRange();
        }
    }

    public void AttackPlayer()
    {
        weapon.Attacking();
    }

    IEnumerable Blooming()
    {

        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
