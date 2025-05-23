using UnityEngine;

public class BOw : MonoBehaviour, IWeapon
{
    [SerializeField]private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIRE_HASH =Animator.StringToHash("Fire");

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();

    }

    public void Attack(){
        
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab,arrowSpawnPoint.position,ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);

    }
     public WeaponInfo GetWeaponInfo(){
        return weaponInfo;
    }

}
