using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public class OnItemAddedEventArgs : EventArgs
    {
        public Item itemAdded;
    }

    public event EventHandler<OnItemAddedEventArgs> OnItemAdded;


    [SerializeField] private Transform ItemsContent;
    [SerializeField] private GameObject InventoryItem;

    [Header("Just for testing purposes!")]
    [SerializeField] private Item[] itemSOs;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        ResetItemSOQuantity();
    }

    private void Start()
    {
        ListItems();
    }

    private void ResetItemSOQuantity()
    {
        foreach (var item in itemSOs)
        {
            item.Quantity = 1;
        }
    }

    public void AddItem(Item item)
    {
        Item desiredItem = null;

        if(item.IsStackable)
        {
            foreach (var itemInInv in Items)
            {
                if (itemInInv.Type == item.Type)
                {
                    itemInInv.Quantity += 1;

                    desiredItem = itemInInv;
                    break;
                }
            }

            if (desiredItem == null)
            {
                Items.Add(item);
            }
        }
        else
        {
            Items.Add(item);
        }

        if (OnItemAdded != null)
            OnItemAdded?.Invoke(this, new OnItemAddedEventArgs { itemAdded = item });

        ListItems();
    }

    public void RemoveItem(Item item)
    {
        if(item.IsStackable)
        {
            item.Quantity -= 1;
            if(item.Quantity < 1)
            {
                item.Quantity = 1;
                Items.Remove(item);
            }
        }
        else
        {
            Items.Remove(item);
        }
    }

    public void ListItems()
    {
        // To avoid multiplication when adding new items to inventory
        foreach (Transform child in ItemsContent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemsContent);

            obj.GetComponent<ItemDeletionButton>().SetItem(item);

            obj.GetComponent<Button>().onClick.AddListener(delegate { OnItemUse(item); });

            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemQuantity = obj.transform.Find("ItemQuantitty").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            int minDisplayQuantity = 1;
            bool displayQuantity = item.Quantity > minDisplayQuantity;

            itemName.text = item.Name;
            itemQuantity.text = displayQuantity ? item.Quantity.ToString() : "";
            itemIcon.sprite = item.Icon;
        }
    }


    private void OnItemUse(Item item)
    {
        UseItem(item);
    }

    private void UseItem(Item item)
    {
        if(item == null)
        {
            Debug.LogError("Item is null");
            return;
        }

        var player = PlayerController.Instance;

        switch (item.Type)
        {
            case Item.ItemType.HealthPotion:
                var playerHealth = player.GetHealthSystem();
                playerHealth.AddHealth(item.Amount);
                break;
            case Item.ItemType.ManaPotion:
                var playerMana = player.GetManaSystem();
                playerMana.AddMana(item.Amount);
                break;
            case Item.ItemType.ExperiencePotion:
                break;
            case Item.ItemType.WoodenSword:
                break;
            case Item.ItemType.IronSword:
                break;
        }

        if (item.IsStackable)
        {
            item.Quantity -= 1;
            if(item.Quantity < 1)
            {
                RemoveItem(item);
            }
        }
        else
        {
            RemoveItem(item);
        }

        ListItems(); // refreshes UI
    }

    public int GetAmountOfCoins()
    {
        int coinsAmount = 0;

        foreach (var item in Items)
        {
            if(item.Type == Item.ItemType.Coin)
            {
                coinsAmount += item.Quantity;
            }
        }

        return coinsAmount;
    }
}
