using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSlayer : Quest
{
    [SerializeField] private List<Item> Reward;

    private void Start()
    {
        QuestName = "UltimateSlayer";
        QuestDescription = "Kill 5 Enemies of particular type";
        ItemsReward.AddRange(Reward);
        ExperienceReward = 36;

        Goals.Add(new KillGoal(this, IEnemy.EnemyType.Type1, "Kill 5 Enemies of particular type", false, 0, 5));
        //Goals.Add(new KillGoal(this, IEnemy.EnemyType.Type1, "Kill 5 Enemies of particular type", false, 0, 5)); // can be another goal

        Goals.ForEach(goal => goal.Init());
    }

}
