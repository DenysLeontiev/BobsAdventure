using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public class OnManaAmountChagedEventArgs : EventArgs
    {
        public int manaPoints;
    }

    public event EventHandler<OnManaAmountChagedEventArgs> OnManaAmountChaged;


    [SerializeField] private int maxMana;
    private int currentMana;

    private void Start()
    {
        currentMana = maxMana;
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }
    public int GetMaxManaAmount()
    {
        return maxMana;
    }

    public void TakeMana(int amount)
    {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maxMana);
        if (OnManaAmountChaged != null)
            OnManaAmountChaged?.Invoke(this, new OnManaAmountChagedEventArgs { manaPoints = currentMana });
    }

    public void AddMana(int amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        if (OnManaAmountChaged != null)
            OnManaAmountChaged?.Invoke(this, new OnManaAmountChagedEventArgs { manaPoints = currentMana });
    }
}
