using UnityEngine;

public class PlayerSpawnner : MonoBehaviour
{
      public GameObject playerPrefab;

    void Awake()
    {
        if (PlayerControler.Instance == null)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
    }
}
