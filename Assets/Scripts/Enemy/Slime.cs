using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Slime : MonoBehaviour, IEnemy
{
    public event Action<IEnemy> OnEnemyDied;

    [SerializeField] private string enemyName;
    public string EnemyName
    {
        get
        {
            return enemyName;
        }
        set
        {
            enemyName = value;
        }
    }

    [SerializeField] private IEnemy.EnemyType enemyType;
    public IEnemy.EnemyType Type
    {
        get
        {
            return enemyType;
        }
        set
        {
            enemyType = value;
        }
    }

    [SerializeField] private int maxEnemyHealth;
    private int currentEnemyHealth;

    private void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
    }

    private void Update()
    {

    }

    public void Die()
    {
        if(OnEnemyDied !=null)
            OnEnemyDied?.Invoke(this);
    }

    public void PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
        currentEnemyHealth = Mathf.Clamp(currentEnemyHealth - amount, 0, maxEnemyHealth);

        if(currentEnemyHealth <= 0) 
        {
            currentEnemyHealth = 0;
            Die();
        }
    }
}
