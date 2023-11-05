using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string QuestDescription { get; set; }
    public int ExperienceReward { get; set; }
    public List<Item> ItemsReward { get; set; } = new List<Item>();
    public bool Completed { get; set; }

    private bool isActive;

    public bool IsQuestActive()
    {
        return isActive;
    }


    public void SetStateActive()
    {
        isActive = true;
    }

    public void ChechGoals()
    {
        Completed = Goals.Any(goal => goal.Completed);

        if(Completed)
        {
            Debug.Log("Reward are given!");
            GiveReward();
        }
    }    

    private void GiveReward()
    {
        throw new System.Exception("Method is not implemented");
    }
}
