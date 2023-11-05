using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public string EnemyName { get; set; }
    public EnemyType Type { get; set; }
    public event Action<IEnemy> OnEnemyDied;

    void Die();
    void TakeDamage(int amount);
    void PerformAttack();

    public enum EnemyType 
    {
        Type1, Type2, Type3, Type4, Type5, Type6
    }

}
