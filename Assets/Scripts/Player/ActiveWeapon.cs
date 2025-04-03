using System;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour currentActiveWeapon {get;private set;}

    private PlayerControles playerControls;

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

    }


    private void Update()
    {
      Attack();  
    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        isAttacking = false; // Reset attack flag
        currentActiveWeapon = newWeapon;

        if (currentActiveWeapon is Sword sword)
        {
            sword.GetComponent<Animator>().Rebind();
            sword.GetComponent<Animator>().Update(0f);
        }
    }




    public void WeaponNull(){
        currentActiveWeapon = null;
    }

    public void ToggleIsAttacking(bool value){
        isAttacking = value;

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
            isAttacking = true;
            weapon.Attack();

        }
    }
}


    
}
