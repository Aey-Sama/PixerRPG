using UnityEngine;

public class WorldObjectScatterer : MonoBehaviour
{
    public GameObject[] spawnPrefabs;
    public int gridResolution = 20;
    public Vector2 areaSize;
    public Vector2 offsetFromCenter;
    public float jitterAmount = 1.2f;
    public float skipChance = 0.2f;

    private bool hasScattered = false;

    void Start()
    {
        if (!hasScattered)
        {
            if (areaSize == Vector2.zero)
            {
                Debug.LogWarning($"{name} has zero areaSize! ChunkManager probably failed to assign it.");
                return;
            }

            ScatterPrefabs();
            hasScattered = true;
        }
    }

    void ScatterPrefabs()
    {
        float cellWidth = areaSize.x / gridResolution;
        float cellHeight = areaSize.y / gridResolution;
        int spawned = 0;

        for (int x = 0; x < gridResolution; x++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
                if (Random.value < skipChance) continue;

                Vector2 gridPos = new Vector2(
                    x * cellWidth - areaSize.x / 2f,
                    y * cellHeight - areaSize.y / 2f
                ) + offsetFromCenter;

                Vector2 jitter = new Vector2(
                    Random.Range(-jitterAmount, jitterAmount),
                    Random.Range(-jitterAmount, jitterAmount)
                );

                Vector3 worldPos = new Vector3(gridPos.x + jitter.x, gridPos.y + jitter.y, 0);

                GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
                Instantiate(prefab, worldPos, Quaternion.identity, transform);
                spawned++;
            }
        }

        Debug.Log($"✅ [{name}] Spawned {spawned} objects.");
    }
}
