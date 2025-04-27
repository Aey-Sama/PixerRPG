using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public int minEnemies = 1;
    public int maxEnemies = 5;
    public Vector2 roomSize = new Vector2(27f, 25f);
    public Vector3 offset = Vector3.zero;
    public float cellSize = 1f;

    private bool hasSpawned = false;
    private bool bossSpawned = false;
    private bool isLastRoom = false;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // [Header("Boss Settings")]
    // public bool allowBossSpawn = true;
    // public GameObject bossPrefab;
    // public Vector3 bossOffset = new Vector3(0, 0, 0);

    

    public void TrySpawnEnemies()
    {
        if (hasSpawned)
            return;

        hasSpawned = true;
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(0.3f);

        RoomTemplates templates = GameObject.FindGameObjectWithTag("Rooms")?.GetComponent<RoomTemplates>();

        if (templates == null)
        {
            Debug.LogError("❌ RoomTemplates not found!");
            yield break;
        }

        GameObject currentRoom = transform.root.gameObject;
        GameObject lastRoom = templates.rooms[templates.rooms.Count - 1];
        RoomController controller = currentRoom.GetComponentInChildren<RoomController>();

        if (currentRoom == lastRoom)
        {
            controller.isLastRoom = true;
        }

        SpawnEnemiesAroundRoom(); // ✅ Only spawn enemies at first
    }

    private void SpawnEnemiesAroundRoom()
    {
        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);
        Vector2 center = transform.position + offset;

        for (int i = 0; i < enemyCount; i++)
        {
            float offsetX = Random.Range(-roomSize.x / 2f, roomSize.x / 2f);
            float offsetY = Random.Range(-roomSize.y / 2f, roomSize.y / 2f);
            Vector2 spawnPos = center + new Vector2(offsetX, offsetY);

            GameObject enemy = Instantiate(
                enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                spawnPos,
                Quaternion.identity,
                transform
            );

            Vector3 parentScale = transform.lossyScale;
            enemy.transform.localScale = new Vector3(
                1f / parentScale.x,
                1f / parentScale.y,
                1f / parentScale.z
            );

            spawnedEnemies.Add(enemy); // ✅ Track spawned enemies
        }
    }


    // private void SpawnBoss()
    // {
    //     if (bossPrefab == null)
    //     {
    //         Debug.LogError(" Boss prefab not assigned!");
    //         return;
    //     }

    //     Vector3 spawnPos = transform.position + bossOffset;
    //     GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity, transform);

    //     Vector3 parentScale = transform.lossyScale;
    //     boss.transform.localScale = new Vector3(
    //         1f / parentScale.x,
    //         1f / parentScale.y,
    //         1f / parentScale.z
    //     );

    //     Debug.Log("👹 Boss Spawned after clearing enemies!");
    // }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + offset;

        Gizmos.color = Color.red;
        for (float x = -roomSize.x / 2f; x <= roomSize.x / 2f; x += cellSize)
        {
            for (float y = -roomSize.y / 2f; y <= roomSize.y / 2f; y += cellSize)
            {
                Vector3 cellCenter = origin + new Vector3(x, y, 0);
                Gizmos.DrawWireCube(cellCenter, Vector3.one * cellSize);
            }
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(origin, new Vector3(roomSize.x, roomSize.y, 0));
    }
}
