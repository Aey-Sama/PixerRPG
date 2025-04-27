using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Chunk Settings")]
    public GameObject chunkPrefab;
    public int chunkSize = 32;
    public int loadRadius = 1; // chunks around player to keep loaded

    [Header("References")]
    public Transform player;

    private Dictionary<Vector2Int, GameObject> loadedChunks = new();

    void Update()
    {
        Vector2Int playerChunk = GetChunkFromPosition(player.position);

        // Load surrounding chunks
        for (int x = -loadRadius; x <= loadRadius; x++)
        {
            for (int y = -loadRadius; y <= loadRadius; y++)
            {
                Vector2Int coord = playerChunk + new Vector2Int(x, y);
                if (!loadedChunks.ContainsKey(coord))
                    LoadChunk(coord);
            }
        }

        // Unload far away chunks
        List<Vector2Int> toUnload = new();
        foreach (var chunk in loadedChunks.Keys)
        {
            if (Vector2Int.Distance(chunk, playerChunk) > loadRadius + 0.5f)
                toUnload.Add(chunk);
        }

        foreach (var coord in toUnload)
        {
            UnloadChunk(coord);
        }
    }

    Vector2Int GetChunkFromPosition(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / chunkSize);
        int y = Mathf.FloorToInt(pos.y / chunkSize);
        return new Vector2Int(x, y);
    }

    void LoadChunk(Vector2Int coord)
    {
        Vector3 worldPos = new Vector3(coord.x * chunkSize, coord.y * chunkSize, 0);
        GameObject chunk = Instantiate(chunkPrefab, worldPos, Quaternion.identity);
        chunk.name = $"Chunk_{coord.x}_{coord.y}";

        // If chunk has scatterer, auto-set its bounds
        var scatterer = chunk.GetComponent<WorldObjectScatterer>();
        if (scatterer)
        {
            scatterer.areaSize = new Vector2(chunkSize, chunkSize);
            scatterer.offsetFromCenter = new Vector2(worldPos.x + chunkSize / 2f, worldPos.y + chunkSize / 2f);
        }

        loadedChunks.Add(coord, chunk);
    }

    void UnloadChunk(Vector2Int coord)
    {
        if (loadedChunks.TryGetValue(coord, out GameObject chunk))
        {
            Destroy(chunk);
            loadedChunks.Remove(coord);
        }
    }
}