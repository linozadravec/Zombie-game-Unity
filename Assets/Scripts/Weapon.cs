using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] float headshotDamageMultiplier = 1.5f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] float shotFrequency = 0.5f;   
    [SerializeField] int moneyPerHit = 10;
    [SerializeField] int headshotMoneyMultiplier = 2;
    [SerializeField] float reloadTime = 2;

    bool isReloading = false;
    bool canShoot = true;
    float headshotDamage;
    AudioSource weaponAudio;
    AudioSource reloadAudio;

    private void OnEnable()
    {
        canShoot = true;
        headshotDamage = damage * headshotDamageMultiplier;
        var sources = GetComponents<AudioSource>();
        weaponAudio = sources[0];
        if (sources.GetUpperBound(0) == 1)
        {
            reloadAudio = sources[1];
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot == true)
        {
            if(ammoSlot != null)
            {
                if(ammoSlot.GetCurrentMagazineBullets() != 0 || gameObject.name == "Pistol - Five Seven")
                {
                    StartCoroutine(Shoot());
                }
                else
                {
                    StartCoroutine(Reload());
                }
            }            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(ammoSlot.GetCurrentAmmo() > 0 && ammoSlot.GetCurrentMagazineBullets() < ammoSlot.GetMagazineSize() && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Reload()
    {
        if (gameObject.name != "Pistol - Five Seven")
        {
            isReloading = true;
            canShoot = false;
            reloadAudio.Play();
            yield return new WaitForSeconds(reloadTime);
            ammoSlot.Reload();
            canShoot = true;
            isReloading = false;
        }
    }

    IEnumerator Shoot()
    {

        canShoot = false;
        if((ammoSlot.GetCurrentMagazineBullets() > 0 || gameObject.name == "Pistol - Five Seven") && !isReloading )
        {  
            ProcessRaycast();

            muzzleFlash.Play();
            weaponAudio.Play();
            ammoSlot.ReduceCurrentAmmo(gameObject.name);      
        }

        yield return new WaitForSeconds(shotFrequency);
        canShoot = true;

    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null && hit.transform.name != "Z_Head") return;

            if(hit.transform.name == "Z_Head")
            {
                target = hit.transform.GetComponentInParent<EnemyHealth>();
                target.TakeDamage(headshotDamage);
                playerMoney.AddMoney(moneyPerHit * headshotMoneyMultiplier);
            }
            else
            {
                target.TakeDamage(damage);
                playerMoney.AddMoney(moneyPerHit);
            }           
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
}
