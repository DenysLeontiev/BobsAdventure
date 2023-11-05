using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTargets : MonoBehaviour
{
    public static CombatTargets Instance;

    public event Action<IEnemy> OnEnemyDied;

    [SerializeField] private List<IEnemy> enemiesList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var enemy in enemiesList)
        {
            enemy.OnEnemyDied += Enemy_OnEnemyDied;
        }
    }

    private void Enemy_OnEnemyDied(IEnemy obj)
    {
        OnEnemyDied?.Invoke(obj);
    }
}
