using UnityEngine;
using System.Collections;


public class DIstructable : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private GameObject[] RandomDrop;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageSourse>() || other.gameObject.GetComponent<Projectile>())
        {
            Instantiate(destroyVFX,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

}
