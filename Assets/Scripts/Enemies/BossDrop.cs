using UnityEngine;

public class BossDrop : MonoBehaviour
{
    public GameObject exitPortalPrefab;
    public Vector3 spawnOffset = new Vector3(0, -1f, 0); // optional

    private EnemyHealth health;

    void Start()
    {
        health = GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.OnDeath += HandleBossDeath;
        }
        else
        {
            Debug.LogWarning("⚠️ BossDrop couldn't find EnemyHealth on " + name);
        }
    }

    private void HandleBossDeath()
    {
        if (exitPortalPrefab != null)
        {
            Instantiate(exitPortalPrefab, transform.position + spawnOffset, Quaternion.identity);
            Debug.Log("🚪 Exit portal spawned by boss drop.");
        }
        else
        {
            Debug.LogWarning("❌ Exit portal prefab not set on BossDrop.");
        }
    }
}
