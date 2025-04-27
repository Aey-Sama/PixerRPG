using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private KnockBack knockback;
    private Flash flash;

    private RoomController roomController;
    public System.Action OnDeath;


    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<KnockBack>();
    }

    private void Start() {
        currentHealth = startingHealth;

         roomController = GetComponentInParent<RoomController>();
        if (roomController != null)
        {
            roomController.RegisterEnemy(this.gameObject);
        }

        
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerControler.Instance.transform,knockBackThrust);
        StartCoroutine(flash.FlashReoutine());
        StartCoroutine(CheckDetectDeathRoutine());

    }

    private IEnumerator CheckDetectDeathRoutine(){
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }


    public void DetectDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFXPrefab,transform.position,Quaternion.identity);

              if (roomController != null)
            {
                roomController.EnemyDied(this.gameObject);
            }
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void ManualRoomAssignment(RoomController room)
    {
        roomController = room;
        roomController.RegisterEnemy(gameObject);
    }


    
}
