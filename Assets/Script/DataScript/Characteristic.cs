using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Characteristic : MonoBehaviour
{
    [Header("Compulsory Component")]
    public Rigidbody2D rg2d;
    public Collider2D col;
    [Header("Character Parameters")]
    public CharacterStats stats;
    public float crHp;

    virtual protected void Start()
    {
        stats.UpdateStats(GameManager.Instance.currentLevel);
        crHp = stats.crHp;

    }
    public float AutoDetect(Collider2D character)
    {
        float distance = Vector2.Distance(character.transform.position, transform.position);
        return distance;
    }
    //
    public void PlayDeath()
    {

        rg2d.Sleep();
        if (this.gameObject.tag == "Enemy")
        {
            GameManager.Instance.spawnsPerRound--;
            
            GameManager.Instance.AddExp(stats.exp);
            
        }
            
    }

    public void TakeDmg(float dmg)
    {
        crHp -= dmg;
        if(this.name == "Player")
        {
            float percent = UIManager.Instance.CalPercentBar(crHp, stats.crHp);
            UIManager.Instance.UpdateBar(percent, UIManager.Instance.hpBar);
        }
        
        
        Debug.Log(gameObject.name + " hp: " + crHp);
        if (crHp <= 0)
            PlayDeath();
    }

}
