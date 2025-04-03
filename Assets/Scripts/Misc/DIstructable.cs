using UnityEngine;
using System.Collections;


public class DIstructable : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private GameObject[] RandomDrop;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageSourse>())
        {
            Instantiate(destroyVFX,transform.position,Quaternion.identity);

              if (RandomDrop.Length > 0)
            {
                int randInt = Random.Range(0, RandomDrop.Length);
                GameObject droppedItem = Instantiate(RandomDrop[randInt], transform.position, Quaternion.identity);

            }
                

            Destroy(gameObject);
        }
        
    }

}
