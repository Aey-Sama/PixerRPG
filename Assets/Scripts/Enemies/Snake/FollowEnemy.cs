using UnityEngine;
using UnityEngine.Timeline;

public class FollowEnemy : MonoBehaviour
{
  public float speed;
  public Transform target;
  public float minimumDistance;


    private void Update()
    {
        if (Vector2.Distance(transform.position,target.position)>minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position,target.position,speed*Time.deltaTime);
            
        }else{
            // attack();
        }
    }
    // private void attack(){
    //     Debug.Log("Snake be attackin shuuuu shuuu!");
    // }
}
