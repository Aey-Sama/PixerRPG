using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private bool triggered = false;

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        // 🔥 Trigger room locking
        RoomController controller = GetComponentInParent<RoomController>();
        if (controller != null)
            controller.PlayerEnteredRoom();

        // 🧟 Trigger enemy spawn
        RoomEnemySpawner enemySpawner = GetComponentInParent<RoomEnemySpawner>();
        ItemSpawnner itemSpawnner = GetComponentInParent<ItemSpawnner>();
        if (enemySpawner != null && itemSpawnner != null)
            enemySpawner.TrySpawnEnemies();
            itemSpawnner.TrySpawnItems();

        triggered = true;
    }

}
