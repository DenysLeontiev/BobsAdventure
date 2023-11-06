using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public class OnQuestActivatedEventArgs: EventArgs
    {
        public Quest quest;
    }

    public event EventHandler<OnQuestActivatedEventArgs> OnQuestActivated;

    [SerializeField] private List<Quest> ActiveQuests;

    private void Awake()
    {
        Instance = this;
    }

    public void AddActiveQuest(Quest questToAdd)
    {
        if (questToAdd == null) return;
        if (questToAdd.IsQuestActive() == true) return;

        questToAdd.SetStateActive();
        ActiveQuests.Add(questToAdd);

        if(OnQuestActivated != null)
            OnQuestActivated?.Invoke(this, new OnQuestActivatedEventArgs { quest = questToAdd });   
    }
}
