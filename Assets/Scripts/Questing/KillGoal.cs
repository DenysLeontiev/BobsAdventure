using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public IEnemy.EnemyType EnemyType { get; set; }

	public KillGoal(Quest quest,IEnemy.EnemyType enemyType, string desctiption, bool completed, int currentAmount, int requiredAmount)
	{
		this.Quest = quest;
		this.EnemyType = enemyType;
		this.Description= desctiption;
		this.Completed = completed;
		this.CurrentAmount = currentAmount;
		this.RequiredAmount = requiredAmount;
	}

    public override void Init()
    {
		CombatTargets.Instance.OnEnemyDied += EnemyDied;
    }

	private void EnemyDied(IEnemy enemy)
	{
		if(enemy.Type == this.EnemyType)
		{
			this.CurrentAmount++;
			Evaluate();
		}
	}
}
