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

    public float AutoDetect(Collider2D character)
    {
        float distance = Vector2.Distance(character.transform.position, transform.position);
        return distance;
    }

    //
    public void PlayDeath()
    {

    }

    public void TakeDmg()
    {

    }

}
