using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerControler : Singleton<PlayerControler>
{

    public bool FacingLeft { get { return facingLeft; }}
    // public static PlayerControler Instance;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;

    //?---------------------------------------

    private PlayerControles playerControles;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private KnockBack knockBack;
    // private Flash flash;
    private float startingMoveSpeed;


    //?---------------------------------------


    private bool facingLeft = false;
    private bool isDashing = false;


    protected override void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        base.Awake();
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        // playerControles.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed;

    }
    private void OnDestroy()
    {
        if (playerControles != null)
        {
            playerControles.Movement.Disable();
            playerControles.Combat.Disable();
            playerControles.Inventory.Disable();
        }
    }



    private void OnEnable()
    {
        if (playerControles == null)
            playerControles = new PlayerControles();

        playerControles.Movement.Enable();
        playerControles.Combat.Enable();
        playerControles.Inventory.Enable();

        playerControles.Combat.Dash.performed += _ => Dash(); // move here
    }



    private void Update() {
        PlayerInput();
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("🎮 Player Pos: " + transform.position);
        }
    }


    private void FixedUpdate() {
        AdjustPlayerFacingFDirection();
        Move();

    }

    private void PlayerInput()
    {
        movement = playerControles.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX",movement.x);
        myAnimator.SetFloat("moveY",movement.y);

    }
    private void Move()
    {
        if(knockBack.GettingKnockedBack){return;}
        rb.MovePosition(rb.position+movement*(moveSpeed*Time.fixedDeltaTime));
    }

    public Transform GetWeaponCollider(){
        return weaponCollider;
    }

    private void AdjustPlayerFacingFDirection()
    {

        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft =true;
        }else{
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
        
    }

    private void Dash(){
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine(){
        float dashTime = 0.2f;
        float dashCD = 0.2f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

    
}
