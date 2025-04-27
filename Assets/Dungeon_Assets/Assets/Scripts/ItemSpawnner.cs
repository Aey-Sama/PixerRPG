using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawnner : MonoBehaviour
{
    public GameObject[] ItemPrefab;

    public int minItem = 1;
    public int maxItem = 5;
    public Vector2 roomSize = new Vector2(27f, 25f);
    public Vector3 offset = Vector3.zero;
    public float cellSize = 1f;

    private bool hasSpawned = false;

    public void TrySpawnItems()
    {
        if (hasSpawned)
            return;

        hasSpawned = true;
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(0.3f);

        SpawnItemsAroundRoom(); 
    }

    private void SpawnItemsAroundRoom()
    {
        int itemsCount = Random.Range(minItem, maxItem + 1);
        Vector2 center = transform.position + offset;

        for (int i = 0; i < itemsCount; i++)
        {
            float offsetX = Random.Range(-roomSize.x / 2f, roomSize.x / 2f);
            float offsetY = Random.Range(-roomSize.y / 2f, roomSize.y / 2f);
            Vector2 spawnPos = center + new Vector2(offsetX, offsetY);

            GameObject enemy = Instantiate(
                ItemPrefab[Random.Range(0, ItemPrefab.Length)],
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

        }
    }
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

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(origin, new Vector3(roomSize.x, roomSize.y, 0));
    }
}
