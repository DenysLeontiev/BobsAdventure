using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinSystem : MonoBehaviour
{
    public static CoinSystem Instance;

    [SerializeField] private int currentCoinsAmount = 0;

    public class OnCoinsAmountChangedEventArgs
    {
        public int coinsAmount;
    }

    public event EventHandler<OnCoinsAmountChangedEventArgs> OnCoinsAmountChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InventoryManager.Instance.OnItemAdded += Instance_OnItemAdded;
    }

    private void Instance_OnItemAdded(object sender, InventoryManager.OnItemAddedEventArgs e)
    {
        if(e.itemAdded!= null)
        {
            if(e.itemAdded.Type == Item.ItemType.Coin)
            {
                currentCoinsAmount++;
            }
        }
    }

    public int GetCurrentCoinsAmount()
    {
        return currentCoinsAmount;
    }

    public void AddCoins(int amount)
    {
        amount = Mathf.Abs(amount);
        currentCoinsAmount += amount;

        if (OnCoinsAmountChanged != null)
            OnCoinsAmountChanged?.Invoke(this, new OnCoinsAmountChangedEventArgs { coinsAmount = currentCoinsAmount });
    }

    public bool TakeCoins(int amount)
    {
        int temp = currentCoinsAmount;
        currentCoinsAmount -= amount;

        if(currentCoinsAmount < 0)
        {
            currentCoinsAmount = temp;
            if (OnCoinsAmountChanged != null)
                OnCoinsAmountChanged?.Invoke(this, new OnCoinsAmountChangedEventArgs { coinsAmount = currentCoinsAmount });
            return false;
        }
        if (OnCoinsAmountChanged != null)
            OnCoinsAmountChanged?.Invoke(this, new OnCoinsAmountChangedEventArgs { coinsAmount = currentCoinsAmount });
        return true;
    }
}
