using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthChanged;

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        if(OnHealthChanged != null) // if we have some listeners
            OnHealthChanged?.Invoke(this,EventArgs.Empty);

        if (currentHealth <= 0)
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
