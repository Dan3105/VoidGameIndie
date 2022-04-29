using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Characteristic
{
    
    public JoystickController joystickController;
    [Header("Dashing component")]
    public float dashForce;

    [Header("Player current state")]
    public bool isAttack;
    public LayerMask whatIsEnemies;

    [Header("Slot Weapon")]
    public SkillWeapon slot1;
    public SkillWeapon slot2;
    

    private void Start()
    {
        slot1.gameObject.SetActive(true);
        slot1.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(joystickController.isDash)
        {
            //rg2d.AddForce(dashForce * joystickController.joystickDir, ForceMode2D.Impulse);
            rg2d.velocity = dashForce * joystickController.joystickDir;
            joystickController.dashTime -= Time.deltaTime;
            col.enabled = false;
            if(joystickController.dashTime <= 0)
            {
                joystickController.isDash = false;
                joystickController.joystickDir = Vector2.zero;
                rg2d.velocity = Vector2.zero;

                col.enabled = true;
            }

        }
        else
        {
            rg2d.velocity = joystickController.joystickDir * stats.speed;
        }

        slot1.DetectRange();

    }

    
}
