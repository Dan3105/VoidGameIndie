using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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

    //[Header("List LevelUp Stats")]
    //public List<SkillSystem.LevelStatus> list = new List<SkillSystem.LevelStatus>();
    //public LevelState[] optBtns = new LevelState[3];
    #endregion

    [Header("GameMachenic Components")]
    public GameObject panelLevelUp;
    public MainCharacterController playerStats; //
    public SkillWeapon weapon;              //  => instead call GetComponent in playerStats

    [Header("Spawning Enemies Machenic")]
    //public AIEnemy monsterDefault;
    public AIEnemy[] enemyList;      //call randomly type of enemy then assign to the monster who gonna spawn
    private ObjectPool<AIEnemy> enemyPool;
    private ObjectPool<GameObject> rockPool;
    public int trackingEnemies;

    [Header("Random Enviroment")]
    public GameObject[] listOfRock;
    [Header("End Scene")]
    public GameObject endScene;

    private void Start()
    {
        endScene.SetActive(false);
        panelLevelUp.SetActive(false);
        //set start level       
        currentLevel = 1;
        experience = 0;
        nextExperience = currentLevel * 100 * 1.25f;
        currentTimeSpawn = timeSpawnDefault;
        trackingEnemies = 0;
        //if playerStats null we have to find and assign it(just in case)
        if (playerStats == null)
        {
            playerStats = GameObject.Find("Player").GetComponent<MainCharacterController>();
            weapon = playerStats.GetComponent<SkillWeapon>();
        }

        //stats for starter level
        PlayerLevelUp();
        AddExp(0);

        //Create ObjectPooling 
        enemyPool = new ObjectPool<AIEnemy>(
            () => { return Instantiate(enemyList[Random.Range(0, enemyList.Length)]); },
            SpawnEnemy,
            monster => { monster.gameObject.SetActive(false); },
            monster => { Destroy(monster.gameObject); },
            false, 30, 100
            );

        rockPool = new ObjectPool<GameObject>(
            () => { return Instantiate(listOfRock[Random.Range(0, listOfRock.Length)]); },
            InitRock,
            rock => { rock.gameObject.SetActive(false); },
            rock => { Destroy(rock.gameObject); },
            false, 30, 2000
            );
        UIManager.Instance.UpdateBar(1f, UIManager.Instance.hpBar);
        InitMap();
        Time.timeScale = 1f;
    }

    public void Update()
    {
        currentTimeSpawn += Time.deltaTime;
        if(currentTimeSpawn >= timeSpawnDefault && playerStats.crHp > 0
            && trackingEnemies < 30 || trackingEnemies == 0)
        {
            int numberEnemy = Random.Range(3 , 4) + currentLevel;
            trackingEnemies += numberEnemy;
            for (int i = 0; i < numberEnemy; i++)
            {
                enemyPool.Get();
            }
            currentTimeSpawn = 0;
        }
    }

    
    #region SpawnMachenic
    public void SpawnEnemy(AIEnemy enemy)
    {
        //Get random direction from player to enemy then make it spawn far a little
        Vector2 randomPos = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        Vector2 dir = ((Vector2)playerStats.transform.position - randomPos).normalized;
        
        //Set stats
        enemy.stats.SetStats(false);
        enemy.stats.UpdateStats(currentLevel);
        enemy.weapon.stats.SetStats();
        enemy.weapon.stats.UpdateStats(currentLevel);

        enemy.gameObject.transform.position = dir * 80;

        enemy.gameObject.SetActive(true);

    }
    #endregion

    #region levelUpMachenic
    public void AddExp(float amount)
    {
        experience += (amount + 0.25f * currentLevel * amount);
        if(experience >= nextExperience)
        {
            //maximum level
            if(currentLevel < 10)
            {
                Time.timeScale = 0f;
                LevelManager.Instance.SetRandomOpt();
                panelLevelUp.SetActive(true);
                currentLevel++;
                PlayerLevelUp();
                //restore hp
                playerStats.crHp += Mathf.Min(playerStats.stats.crHp, playerStats.stats.crHp * 0.4f);

                nextExperience = 400 * (currentLevel * currentLevel) / 5;
                experience = nextExperience - experience;
                
            }

            // restore full hp

        }
        float percent = UIManager.Instance.CalPercentBar(experience, nextExperience);
        UIManager.Instance.UpdateBar(percent, UIManager.Instance.expBar);
        Debug.Log("Player level: " + currentLevel + " Exp: " + experience + " NextLV: " + nextExperience);
    }



    public void PlayerLevelUp()
    {

        //update new stats of character
        playerStats.stats.UpdateStats(currentLevel);
        
        
        
        //update stats of player weapon;
        weapon.stats.UpdateStats(currentLevel);
        
        //Update UI
        float percent = UIManager.Instance.CalPercentBar(playerStats.crHp, playerStats.stats.crHp);
        UIManager.Instance.UpdateBar(percent, UIManager.Instance.hpBar);
        
    }

    #endregion

    void InitRock(GameObject thisRock)
    {

        int x = Random.Range(-150, 150);
        int y = Random.Range(-150, 150);
        Vector2 pos = new Vector2(x, y);
        Collider2D[] check = Physics2D.OverlapCircleAll(pos, 5f);
        foreach (var rock in check)
            if (rock.gameObject.tag == "Rock")
                Destroy(rock.gameObject);
        thisRock.transform.position = new Vector2(x, y);
        thisRock.SetActive(true);
        thisRock.transform.parent = this.gameObject.transform;
    }

    void InitMap()
    {
        for (int i = 0; i < 1000; i++)
            rockPool.Get();
    }
}
