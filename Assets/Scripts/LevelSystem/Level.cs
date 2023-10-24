using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public class OnLevelUpEventArgs : EventArgs
    {
        public int level;
    }

    public class OnExperienceAddedEventArgs : EventArgs
    {
        public float experienceToNextLevelNormalized;
    }

    public int experience;
    public int currentLevel;

    public event EventHandler<OnLevelUpEventArgs> OnLevelUp;
    public event EventHandler<OnExperienceAddedEventArgs> OnExperienceAdded;

    public int MAX_EXP;
    public int MAX_LEVEL = 99;

    public Level(int level)
    {
        currentLevel = level;
        experience = GetXPForLevel(level);
        MAX_EXP = GetXPForLevel(MAX_LEVEL);
    }

    public int GetXPForLevel(int level)
    {
        if(level > MAX_LEVEL)
        {
            return 0;
        }

        int firstPass = 0;
        int secondPass = 0;
        for (int levelCycle = 1; levelCycle < level; levelCycle++)
        {
            firstPass += (int)Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, (levelCycle / 7.0f))));
            secondPass = firstPass / 4;
        }

        if(secondPass > MAX_EXP && MAX_EXP != 0)
        {
            return MAX_EXP;
        }

        if(secondPass < 0)
        {
            return MAX_EXP;
        }

        return secondPass;
    }

    public int GetLevelForXP(int exp)
    {
        if(exp > MAX_EXP)
        {
            return MAX_LEVEL;
        }

        int firstPass = 0;
        int secondPass = 0;

        for (int levelCycle = 1; levelCycle < MAX_LEVEL; levelCycle++)
        {
            firstPass += (int)Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, (levelCycle / 7.0f))));
            secondPass = firstPass / 4;
            if(secondPass > exp)
            {
                return levelCycle;
            }
        }

        if(exp > secondPass)
        {
            return MAX_LEVEL;
        }

        return 0;
    }

    public bool AddExperience(int amount)
    {
        if(amount + experience < 0 || experience > MAX_EXP)
        {
            if(experience > MAX_EXP)
            {
                experience = MAX_EXP;
            }
            return false;
        }

        int oldLevel = GetLevelForXP(experience);
        experience += amount;

        if(OnExperienceAdded != null)
        {
            OnExperienceAdded?.Invoke(this, new OnExperienceAddedEventArgs { experienceToNextLevelNormalized = (float) experience / GetXPForLevel(currentLevel + 1)});
        }

        if(oldLevel < GetLevelForXP(experience))
        {
            if(currentLevel < GetLevelForXP(experience))
            {
                currentLevel = GetLevelForXP(experience);
                if(OnLevelUp != null)
                {
                    OnLevelUp?.Invoke(this, new OnLevelUpEventArgs { level = currentLevel });
                }

                if (OnExperienceAdded != null)
                {
                    OnExperienceAdded?.Invoke(this, new OnExperienceAddedEventArgs { experienceToNextLevelNormalized = (float) experience / GetXPForLevel(currentLevel + 1) });
                }

                return  true;
            }
        }

        return false;
    }
}
