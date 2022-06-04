using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : Characteristic
{
    #region singleton
    private static MainCharacterController instance;
    public static MainCharacterController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as MainCharacterController;
        }
        else
            Destroy(gameObject);


    }
    #endregion

    public JoystickController joystickController;
    [SerializeField] LevelSystem levelSystem;
    [Header("Dashing component")]
    public float dashForce;
    public float cdDashDefault;
    private float spDash;
    private float cdDash;

    [Header("Player current state")]
    public bool isAttack;
    public LayerMask whatIsEnemies;

    [Header("Slot Weapon")]
    public SkillWeapon weapon;

    [Header("Animator")]
    public SpriteRenderer sprite;
    public Animator animator;

    
    override protected void Start()
    {
        base.Start();
        stats.SetStats(true);
        weapon.stats.SetStats();
        weapon.gameObject.SetActive(true);
        animator.runtimeAnimatorController = stats.animator;
        cdDash = 0f;
        spDash = 0f;
    }
    private void Update()
    {
        weapon.cdCall -= Time.deltaTime;
        if(rg2d.velocity != Vector2.zero)
        {
            animator.SetFloat("Horizontal", rg2d.velocity.x);
            animator.SetFloat("Vertical", rg2d.velocity.y);
            if (rg2d.velocity.x > 0)
                sprite.flipX = false;
            else if (rg2d.velocity.x < 0)
                sprite.flipX = true;
        }
        
        animator.SetFloat("Velocity", rg2d.velocity.sqrMagnitude);

    }
    private void FixedUpdate()
    {
        
        if (crHp >= 0)
        {
            if (joystickController.isDash && cdDash < 0f)
            {
                rg2d.velocity = dashForce * joystickController.joystickDir;
                joystickController.dashTime -= Time.deltaTime;

                if (joystickController.dashTime <= 0)
                {
                    joystickController.isDash = false;
                    joystickController.joystickDir = Vector2.zero;
                    rg2d.velocity = Vector2.zero;
                }
                cdDash = cdDashDefault - spDash;
            }
            else
                rg2d.velocity = joystickController.joystickDir * stats.speed;
            
            
            weapon.DetectRange();
        }
        

    }

   public void SetDash(float percent)
    {
        spDash += cdDashDefault*percent ;
    }
}
