using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
   public float speed;
  public Transform target;
  public float minimumDistance;

  public GameObject projectile;
  public float timeBetweenShots;
  private float nextShotTime;



    private void Update()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(projectile,transform.position, Quaternion.identity);
            nextShotTime = Time.time +timeBetweenShots;
        }
        if (Vector2.Distance(transform.position,target.position)< minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position,target.position,-speed*Time.deltaTime);
            
        }else{
            // attack();
        }
    }
    // private void attack(){
    //     Debug.Log("Snake be attackin shuuuu shuuu!");
    // }
}
