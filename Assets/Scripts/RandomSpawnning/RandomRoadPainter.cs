using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class RandomWalk2x2PathPainter : MonoBehaviour
{
    public Tilemap roadsTilemap;      // Assign manually or auto-find
    public TileBase roadRuleTile;     // Your RuleTile for ponds or decor
    public int steps = 100;

    private HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

    void Start()
    {
        // 🔍 Auto-find "Surrounding" tilemap if not assigned
        if (roadsTilemap == null)
        {
            GameObject obj = GameObject.Find("Surrounding");
            if (obj != null)
                roadsTilemap = obj.GetComponent<Tilemap>();
            else
                Debug.LogError("❌ Tilemap 'Surrounding' not found!");
        }

        if (roadsTilemap == null || roadRuleTile == null)
        {
            Debug.LogError("❌ Missing roadsTilemap or roadRuleTile — cannot run painter!");
            return;
        }

        Vector3 worldPos = transform.position;
        Vector3Int current = roadsTilemap.WorldToCell(worldPos);

        GenerateRandomWalkPath(current);
        roadsTilemap.RefreshAllTiles();
    }

    void GenerateRandomWalkPath(Vector3Int start)
    {
        Vector3Int current = start;

        for (int i = 0; i < steps; i++)
        {
            Stamp2x2(current);
            current += GetRandomStep();
        }
    }

    void Stamp2x2(Vector3Int center)
    {
        for (int x = 0; x <= 1; x++)
        {
            for (int y = 0; y <= 1; y++)
            {
                Vector3Int tilePos = center + new Vector3Int(x, y, 0);
                if (!visited.Contains(tilePos))
                {
                    roadsTilemap.SetTile(tilePos, roadRuleTile);
                    visited.Add(tilePos);
                }
            }
        }
    }

    Vector3Int GetRandomStep()
    {
        int dir = Random.Range(0, 4);
        return dir switch
        {
            0 => Vector3Int.up,
            1 => Vector3Int.down,
            2 => Vector3Int.left,
            3 => Vector3Int.right,
            _ => Vector3Int.right
        };
    }
}
