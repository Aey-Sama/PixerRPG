using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Collider2D[] doorBlockers; // Locking walls
    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool roomStarted = false;

    [Header("Room Lock Settings")]
    public float lockDelay = 0.5f;

    [Header("Camera Shake (Optional)")]
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    [Header("Boss Settings")]
    public bool isLastRoom = false;
    public bool allowBossSpawn = true;
    public GameObject bossPrefab;
    public Vector3 bossOffset = Vector3.zero;
    private bool bossSpawned = false;


    public void PlayerEnteredRoom()
    {
        if (roomStarted) return;

        roomStarted = true;
        Debug.Log("🧟 Player entered room — locking will start after delay...");

        StartCoroutine(LockDoorsWithDelay());
    }

    private IEnumerator LockDoorsWithDelay()
    {
        yield return new WaitForSeconds(lockDelay);

        foreach (var door in doorBlockers)
        {
            if (door == null)
            {
                Debug.LogWarning("🚫 Null door in doorBlockers!");
                continue;
            }

            door.isTrigger = false; // 🔒 make door solid
            door.enabled = true;

            Debug.Log("🔒 Door turned solid: " + door.name);
        }

        // Trigger camera shake
        CameraShake.Instance?.Shake(shakeDuration, shakeMagnitude); // ✅ safe check if camera shake exists
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!activeEnemies.Contains(enemy))
        {
            activeEnemies.Add(enemy);
            Debug.Log("🧠 Enemy registered. Total: " + activeEnemies.Count);
            Debug.Log($"📦 Registered Enemy: {enemy.name} in room: {name}");

        }
    }

    public void EnemyDied(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        Debug.Log("💀 Enemy died. Remaining: " + activeEnemies.Count);

        if (activeEnemies.Count <= 0)
        {
            if (isLastRoom && allowBossSpawn && !bossSpawned)
            {
                SpawnBoss();
                bossSpawned = true;
            }
            else
            {
                UnlockDoors();
            }
        }
    }

    private void SpawnBoss()
    {

        Vector3 spawnPos = transform.position + bossOffset;
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity, transform);
    }



    private void UnlockDoors()
    {
        Debug.Log("✅ Room cleared! Unlocking doors.");

        foreach (var door in doorBlockers)
        {
            if (door != null)
            {
                door.isTrigger = true;
                Debug.Log("🔓 Door unlocked: " + door.name);
            }
        }
    }
}
