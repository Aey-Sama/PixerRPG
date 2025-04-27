using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private string healthTextObjectName = "PlayerHealth"; 
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private GameObject deathVFXPrefab;

    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;
    private TextMeshProUGUI healthText;

    private void Awake()
    {
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();

        GameObject healthTextObject = GameObject.Find(healthTextObjectName);
        if (healthTextObject != null)
        {
            healthText = healthTextObject.GetComponent<TextMeshProUGUI>();
        }

        UpdateHealthUI();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
        if (enemy != null && canTakeDamage)
        {
            TakeDamage(1);
            knockBack.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashReoutine());
        }
    }

    private void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
        currentHealth -= damageAmount;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die(); // ✅ Call death method
        }
        else
        {
            StartCoroutine(DamageRecoveryRoutine());
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP : " + currentHealth.ToString();
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;

    }
    private void Die()
    {
        Instantiate(deathVFXPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject); 
        SceneManager.LoadScene("MainMenu");

    }




}
