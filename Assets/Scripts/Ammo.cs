using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 90;
    [SerializeField] TextMeshProUGUI ammoText;

    private int magazineSize;
    private int magazineBullets;

    public int GetCurrentAmmo()
    {
        return ammoAmount;
    }
    
    public int GetCurrentMagazineBullets()
    {
        return magazineBullets;
    }

    public int GetMagazineSize()
    {
        return magazineSize;
    }

    public void SetAmmoAmount(int boughtAmmo, int boughtMagazineSize)
    {
        ammoAmount = boughtAmmo;
        magazineSize = boughtMagazineSize;
        magazineBullets = boughtMagazineSize;

        AmmoGUIUpdate();
    }

    public void AmmoGUIUpdate()
    {
        ammoText.text = magazineBullets.ToString() + "/" + ammoAmount.ToString();
        if(magazineBullets < magazineSize * 0.25)
        {
            ammoText.color = new Color(200, 0, 0, 255);
        }
        else
        {
            ammoText.color = new Color(255, 255, 255, 255);
        }
    }

    public void ReduceCurrentAmmo(string weaponName)
    {
        if(weaponName != "Pistol - Five Seven")
        {
            magazineBullets--;
            AmmoGUIUpdate();
        }
        else
        {
            ammoText.text = "\u221e/\u221e";
        }
    }

    public void Reload()
    {
        if(ammoAmount - magazineSize >= 0)
        {
            ammoAmount -= magazineSize;
            magazineBullets = magazineSize;         
            AmmoGUIUpdate();      
        }
        else
        {
            magazineBullets = ammoAmount;
            ammoAmount = 0;
            AmmoGUIUpdate();
        }
    }
}
