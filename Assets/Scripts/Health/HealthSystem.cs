using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }


    /// <summary>
    /// Used for Health PowerUp
    /// </summary>
    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
    }
}
