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
    public SkillWeapon weapon;

    [Header("Animator")]
    public Animator animator;
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
        weapon.gameObject.SetActive(true);
        animator.runtimeAnimatorController = stats.animator;
    }
    private void Update()
    {
        weapon.cdCall -= Time.deltaTime;
        if(rg2d.velocity != Vector2.zero)
        {
            animator.SetFloat("Horizontal", rg2d.velocity.x);
            animator.SetFloat("Vertical", rg2d.velocity.y);
        }
        
        animator.SetFloat("Velocity", rg2d.velocity.sqrMagnitude);

    }
    private void FixedUpdate()
    {
        
        if (isAlive)
        {
            if (joystickController.isDash)
            {
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
            
            
            weapon.DetectRange();
        }
        

    }

  
}
