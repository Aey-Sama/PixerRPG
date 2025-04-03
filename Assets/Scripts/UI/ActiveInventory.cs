using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInventory : MonoBehaviour
{
    private int ActiveSlotIndexNum = 0;

    private PlayerControles playerControls;


    private void Awake()
    {
        playerControls = new PlayerControles();
    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ToggleActiveHighLight(0);
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue -1);
    }

    private void ToggleActiveHighLight(int indexNum)
    {
        ActiveSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChnageActiveWeapon();
    }

   private void ChnageActiveWeapon()
    {
        if (ActiveWeapon.Instance.currentActiveWeapon != null)
        {
            Debug.Log("Destroying old weapon: " + ActiveWeapon.Instance.currentActiveWeapon.name);
            Destroy(ActiveWeapon.Instance.currentActiveWeapon.gameObject);
        }

        if (!transform.GetChild(ActiveSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            Debug.LogError("No weapon found in inventory slot!");
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(ActiveSlotIndexNum)
            .GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity, ActiveWeapon.Instance.transform);
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());

        Debug.Log("New weapon equipped: " + newWeapon.name);
    }


}
