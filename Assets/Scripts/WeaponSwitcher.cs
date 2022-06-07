using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    [SerializeField] int boughtWeapon = 0;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Ammo ammo;

    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWheel();
        if(previousWeapon != currentWeapon)
        {
            SetWeaponActive();   
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
                if (weapon.gameObject.name != "Pistol - Five Seven")
                {
                    ammo.AmmoGUIUpdate();
                }
                else
                {
                    ammoText.text = "\u221e/\u221e";
                    ammoText.color = new Color(255, 255, 255, 255);
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (currentWeapon == 0)
            {
                currentWeapon = boughtWeapon;
            }
            else
            {
                currentWeapon = 0;
            }
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = boughtWeapon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 0;
        }
    }

    public void SetBoughtWeapon(int boughtWeaponIndex)
    {    
        boughtWeapon = boughtWeaponIndex;
        currentWeapon = boughtWeapon;
        
        SetWeaponActive();
    }
}
