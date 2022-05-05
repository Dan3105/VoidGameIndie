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
    public bool isAlive;
    virtual protected void Start()
    {
        crHp = stats.hp;
        isAlive = true;
    }
    public float AutoDetect(Collider2D character)
    {
        float distance = Vector2.Distance(character.transform.position, transform.position);
        return distance;
    }
    //
    public void PlayDeath()
    {
        isAlive = false;
        rg2d.Sleep();
        if (this.gameObject.tag == "Enemy")
        {
            GameManager.Instance.spawnsPerRound--;
            Destroy(this.gameObject);
            GameManager.Instance.AddExp(stats.exp);
        }
            
    }

    public void TakeDmg(float dmg)
    {
        crHp -= dmg;
        Debug.Log(gameObject.name + " hp: " + crHp);
        if (crHp <= 0)
            PlayDeath();
    }

    public void RotatePlayer(bool isRight)
    {
        int dir = isRight ? 1 : -1;
        transform.localScale = new Vector2(dir * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        
    }
}
