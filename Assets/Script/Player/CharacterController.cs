using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Characteristic
{
    #region singleton
    private static CharacterController instance;
    public static CharacterController Instance { get; private set; }
    #endregion

    public JoystickController joystickController;
    [Header("Dashing component")]
    public float dashForce;

    [Header("Player current state")]
    public bool isAttack;
    public LayerMask whatIsEnemies;

    [Header("Slot Weapon")]
    public SkillWeapon slot1;
    #region singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as CharacterController;
        }
        else
            Destroy(gameObject);
    }
    #endregion
    override protected void Start()
    {
        base.Start();
        slot1.gameObject.SetActive(true);
        
    }
    private void Update()
    {
        slot1.cdCall -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        
        if (isAlive)
        {
            if (joystickController.isDash)
            {
                //rg2d.AddForce(dashForce * joystickController.joystickDir, ForceMode2D.Impulse);
                rg2d.velocity = dashForce * joystickController.joystickDir;
                joystickController.dashTime -= Time.deltaTime;
                col.enabled = false;
                if (joystickController.dashTime <= 0)
                {
                    joystickController.isDash = false;
                    joystickController.joystickDir = Vector2.zero;
                    rg2d.velocity = Vector2.zero;

                    col.enabled = true;
                }

            }
            else
                rg2d.velocity = joystickController.joystickDir * stats.speed;

            slot1.DetectRange();
        }
        

    }

  
}
