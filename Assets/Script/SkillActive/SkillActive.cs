using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActive : MonoBehaviour
{
    public CharacterController character;
    public void PlayAttack()
    {
        //if (character.weapon.dir.magnitude > 0)
        //{
        character.animator.SetFloat("AtkHorizontal", character.weapon.dir.x);
        character.animator.SetFloat("AtkVertical", character.weapon.dir.y);
        character.animator.SetTrigger("Attack");
        //}

    }
}
