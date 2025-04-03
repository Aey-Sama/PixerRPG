using UnityEngine;

public class BOw : MonoBehaviour, IWeapon
{
    public void Attack(){
        Debug.Log("Bow Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
