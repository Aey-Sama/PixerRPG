using System;
using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour currentActiveWeapon {get;private set;}

    private PlayerControles playerControls;
    private float timeBetweenAttacks;

    private bool attackButtonDown, isAttacking = false;

    protected override void Awake(){
        base.Awake();

        playerControls = new PlayerControles();


    }
    private void OnEnable()
    {
      playerControls.Enable();    
    }

    private void Start()
    {
      playerControls.Combat.Attack.started += _ => StartAttacking();
      playerControls.Combat.Attack.canceled += _ => StopAttacking();

      AttackCooldown();

    }


    private void Update()
    {
      Attack();  
    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        isAttacking = false; // Reset attack flag
        currentActiveWeapon = newWeapon;

        AttackCooldown();
        timeBetweenAttacks = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;

        if (currentActiveWeapon is Sword sword)
        {
            sword.GetComponent<Animator>().Rebind();
            sword.GetComponent<Animator>().Update(0f);
        }
    }




    public void WeaponNull(){
        currentActiveWeapon = null;
    }

  
    public void AttackCooldown(){
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

  private void Attack()
{
    if (attackButtonDown && !isAttacking)
    {
        if (currentActiveWeapon is IWeapon weapon)
        {
            AttackCooldown();
            weapon.Attack();

        }
    }
}


    
}
