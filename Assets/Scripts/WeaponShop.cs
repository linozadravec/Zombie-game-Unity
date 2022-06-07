using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] Canvas shopCanvas;
    [SerializeField] TextMeshProUGUI buyTextMesh;
    [SerializeField] int weaponPrice = 300;
    [SerializeField] int boughtWeaponIndex = 1;
    [SerializeField] int ammoAmount = 90;
    [SerializeField] int magazineSize = 30;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] WeaponSwitcher playerWeaponSwitcher;
    [SerializeField] Ammo ammo;

    bool canBuy = true;

    bool isColliding = false;

    

    private void Start()
    {
        shopCanvas.enabled = false;
    }

    private void Update()
    {
        if(isColliding)
        {
            if (Input.GetKeyDown(KeyCode.F) && canBuy)
            {
                if (playerMoney.MoneyAmount() >= weaponPrice)
                {
                    StartCoroutine(BuyWeapon());
                }
            }
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {        
        shopCanvas.enabled = true;
        buyTextMesh.text = "Press F to buy a weapon\n" + "\t   ($" + weaponPrice + ")";
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        shopCanvas.enabled = false;
    }

    IEnumerator BuyWeapon()
    {
        canBuy = false;
        playerMoney.SubtractMoney(weaponPrice);
        playerWeaponSwitcher.SetBoughtWeapon(boughtWeaponIndex);
        ammo.SetAmmoAmount(ammoAmount, magazineSize);
        yield return new WaitForSeconds(2);
        canBuy = true;
    }
}
