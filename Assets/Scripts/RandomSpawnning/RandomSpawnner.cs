using UnityEngine;

public class RandomSpawnner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;

    void Start()
    {
        SpawnRandomObject();
    }

    void SpawnRandomObject()
    {
        if (objectsToSpawn.Length == 0) return;

        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject randomObject = objectsToSpawn[randomIndex];

        Instantiate(randomObject, transform.position, Quaternion.identity);
    }
}
