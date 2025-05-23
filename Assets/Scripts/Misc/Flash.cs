using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = 0.2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    // private EnemyHealth enemyHealth;

    private void Awake()
    {
        // enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat =  spriteRenderer.material;

    }

    public float GetRestoreMatTime(){
        return restoreDefaultMatTime;
    }

    public IEnumerator FlashReoutine(){
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
        // enemyHealth.DetectDeath();
    }


}
