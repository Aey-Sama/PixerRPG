using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Vector3 targetPosition;
    public Transform Player;
    public float speed;
    public void Start()
    {
        targetPosition = Player.transform.position;
    }

    private void Update()
    {
        targetPosition = Vector2.MoveTowards(transform.position,targetPosition,speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }

    }

}
