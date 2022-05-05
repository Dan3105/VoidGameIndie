using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class GameManager : SingletonTemplate<GameManager>
{
    [Header("Level GamePlay")]
    #region LevelGamePlay

    //spawing enemy
    public float timeSpawnDefault;
    public float currentTimeSpawn;
    #endregion
    [Header("Level Player")]
    #region LevelPlayer
    public int currentLevel = 1;            // max level = 10
    public float experience = 0;            //
    public float nextExperience;            // 

    public int levelSaving;                 //support for skilltree
    #endregion

    [Header("GameMachenic Components")]
    public CharacterController playerStats; //
    public SkillWeapon weapon;              //  => instead call GetComponent in playerStats

    [Header("Spawning Enemies Machenic")]
    public AIEnemy monsterDefault;
    public SkillWeapon[] typeWeapon;         // decide enemy/monster use melee weapon or rangeWeapon
    public WeaponStats[] meleeList;         //call randomly melee Weapon in the list
    public WeaponStats[] rangeList;         //call randomly range Weapon in the list
    public CharacterStats[] enemyStatsList;      //call randomly type of enemy then assign to the monster who gonna spawn
    public int spawnsPerRound;
    private ObjectPool<AIEnemy> enemyPool;
    private void Start()
    {
        //set start level       
        currentLevel = 1;
        experience = 0;
        nextExperience = currentLevel * 100 * 1.25f;
        currentTimeSpawn = timeSpawnDefault;

        //if playerStats null we have to find and assign it(just in case)
        if (playerStats == null)
        {
            playerStats = GameObject.Find("Player").GetComponent<CharacterController>();
            weapon = playerStats.GetComponent<SkillWeapon>();
        }
       
        //stats for starter level
        UpdateStatsPlayer();

        //Create ObjectPooling 
        enemyPool = new ObjectPool<AIEnemy>(
            ListEnemyStarter,
            SpawnEnemy,
            monster => { monster.gameObject.SetActive(false); },
            monster => { Destroy(monster.gameObject); },
            false, 30, 100
            );    

        spawnsPerRound = 0;
    }

    public void Update()
    {
        currentTimeSpawn += Time.deltaTime;
        if(currentTimeSpawn >= timeSpawnDefault && spawnsPerRound == 0)
        {
            int numberEnemy = Random.Range(3, 6);
            spawnsPerRound = numberEnemy;
            for (int i = 0; i < numberEnemy; i++)
            {
                enemyPool.Get();
            }
            currentTimeSpawn = 0;
        }
    }

    public AIEnemy ListEnemyStarter()
    {
        var enemy = Instantiate(monsterDefault);
        UpdateRandomEnemy(ref enemy);
        return enemy;
    }

    public void SpawnEnemy(AIEnemy enemy)
    {
        //Get random direction from player to enemy then make it spawn far a little
        Vector2 randomPos = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        Vector2 dir = ((Vector2)playerStats.transform.position - randomPos).normalized;
        enemy.gameObject.transform.position = dir * 30;

        UpdateRandomEnemy(ref enemy);
        enemy.gameObject.SetActive(true);
    }



    public void AddExp(float amount)
    {
        experience += (amount + 0.25f * currentLevel * amount);
        if(experience >= nextExperience)
        {
            currentLevel++;
            weapon.stats.UpdateStats(currentLevel);
            nextExperience = currentLevel * 100 * 1.25f;
            experience = nextExperience - experience;
            
        }
        Debug.Log("Player level: " + currentLevel + " Exp: " + experience + " NextLV: " + nextExperience);
    }

    public void UpdateStatsPlayer()
    {
        weapon.stats.UpdateStats(currentLevel);
    }

    public void UpdateRandomEnemy(ref AIEnemy enemy)
    {
        //call random type of weapon
        enemy.weapon = typeWeapon[Random.Range(0, typeWeapon.Length)];

        //depend which type of weapon then set stats
        if (enemy.weapon.isMelee)
        {
            enemy.weapon.stats = meleeList[Random.Range(0, meleeList.Length)];
        } 
        else
        {
            enemy.weapon.stats = rangeList[Random.Range(0, rangeList.Length)];
        }
            

        //set stats of enemy
        enemy.stats = enemyStatsList[Random.Range(0, enemyStatsList.Length)];
        
    }
}
