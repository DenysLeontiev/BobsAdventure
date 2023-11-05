using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quest consists of multiple (or one) goals
/// </summary>
public class Goal
{
    public Quest Quest { get; set; } // which Quest a Goal is assigned to
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Init()
    {
        // default init stuff
    }

    public void Evaluate()
    {
        if(CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Quest.ChechGoals(); // check if this Goal completion is enough to complete the whole Quest
        Completed = true;
    }
}
