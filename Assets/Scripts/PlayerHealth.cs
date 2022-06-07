using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    [SerializeField] float regenerationRate = 1f;
    [SerializeField] float regenerationAmount = 1f;
    [SerializeField] float timeToStartRegeneration = 5f;

    [SerializeField] HealthBar healthbar;
    [SerializeField] TextMeshProUGUI healthNumberText;

    private float hitPoints = 100f;
    private float timestamp = 0.0f;

    private void Start()
    {
        InvokeRepeating("Regeneration", 0, regenerationRate);
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        timestamp = Time.time;
        UpdateHealthBarGUI();

        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
  
    void Regeneration()
    {
        if (hitPoints < maxHitPoints && Time.time > (timestamp + timeToStartRegeneration))
        {
            hitPoints += regenerationAmount;
            UpdateHealthBarGUI();
        }
    }

    private void UpdateHealthBarGUI()
    {
        healthbar.SetHealth(hitPoints);
        healthNumberText.text = hitPoints.ToString();
    }
}
