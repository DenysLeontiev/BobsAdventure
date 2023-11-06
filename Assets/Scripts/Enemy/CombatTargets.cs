using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTargets : MonoBehaviour
{
    public static CombatTargets Instance;

    public event Action<IEnemy> OnEnemyDied;

    [SerializeField] private List<WaveSystemSO> enemyWaves;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var waveSO in enemyWaves)
        {
            foreach (var enemy in waveSO.enemyWave)
            {
                enemy.OnEnemyDied += Enemy_OnEnemyDied;
            }
        }
    }

    private void Enemy_OnEnemyDied(IEnemy obj)
    {
        OnEnemyDied?.Invoke(obj);
    }
}
