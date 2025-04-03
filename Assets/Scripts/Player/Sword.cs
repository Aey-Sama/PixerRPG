using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.5f;
    // [SerializeField] private Transform weaponCollider;



    private Animator myAnimator;
    private GameObject slashAnim;

    private Transform weaponCollider;



    private  void Awake()
    {


        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = PlayerControler.Instance.GetWeaponCollider();
        
        // Ensure the slash animation spawn point exists
        if (slashAnimSpawnPoint == null)
        {
            slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint")?.transform;
            if (slashAnimSpawnPoint == null)
            {
                Debug.LogError("SlashSpawnPoint not found in the scene!");
            }
        }
    }


    private void Update()
    {
        MouseFollowWithOffset();

    }


   public void Attack()
    {
        if (myAnimator == null || slashAnimPrefab == null || slashAnimSpawnPoint == null)
        {
            Debug.LogError("Sword Attack() called, but some components are missing!");
            return;
        }

        Debug.Log("Sword Attack() called! Animator: " + (myAnimator != null));

        myAnimator.ResetTrigger("Attack");
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        if (slashAnimPrefab != null)
        {
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
        }
        else
        {
            Debug.LogError("Slash animation prefab is missing!");
        }

        StartCoroutine(AttackCDRoutine());
    }




    private IEnumerator AttackCDRoutine(){
        yield return new WaitForSeconds(swordAttackCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }

    public void DoneAttackingAnimEvent() {
        weaponCollider.gameObject.SetActive(false);
    }

      public void SwingUpFlipAnimEvent() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerControler.Instance.FacingLeft) { 
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
      }
        public void SwingDownFlipAnimEvent() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerControler.Instance.FacingLeft) { 
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    
    
    }

     private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerControler.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }
}
