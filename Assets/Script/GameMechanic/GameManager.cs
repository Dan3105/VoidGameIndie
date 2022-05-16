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
    //public AIEnemy monsterDefault;
    public AIEnemy[] enemyList;      //call randomly type of enemy then assign to the monster who gonna spawn
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
            () => { return Instantiate(enemyList[Random.Range(0, enemyList.Length)]); },
            SpawnEnemy,
            monster => { monster.gameObject.SetActive(false); },
            monster => { Destroy(monster.gameObject); },
            false, 30, 100
            );    

        spawnsPerRound = 0;

        //Set stats Bar 
        UIManager.Instance.UpdateBar(1, UIManager.Instance.hpBar);
        UIManager.Instance.UpdateBar(0, UIManager.Instance.expBar);
    }

    public void Update()
    {
        currentTimeSpawn += Time.deltaTime;
        if(currentTimeSpawn >= timeSpawnDefault && spawnsPerRound == 0)
        {
            int numberEnemy = Random.Range(3 , 6);
            spawnsPerRound = numberEnemy;
            for (int i = 0; i < numberEnemy; i++)
            {
                enemyPool.Get();
            }
            currentTimeSpawn = 0;
        }
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
            nextExperience = 500 * (currentLevel * currentLevel) - (500 * currentLevel);
            experience = nextExperience - experience;
           
        }
        float percent = UIManager.Instance.CalPercentBar(experience, nextExperience);
        UIManager.Instance.UpdateBar(percent, UIManager.Instance.expBar);
        Debug.Log("Player level: " + currentLevel + " Exp: " + experience + " NextLV: " + nextExperience);
    }

    public void UpdateStatsPlayer()
    {
        weapon.stats.UpdateStats(currentLevel);
        float percent = UIManager.Instance.CalPercentBar(playerStats.crHp, playerStats.stats.crHp);
        UIManager.Instance.UpdateBar(percent, UIManager.Instance.hpBar);
    }

    public void UpdateRandomEnemy(ref AIEnemy enemy)
    {
        
    }
}
