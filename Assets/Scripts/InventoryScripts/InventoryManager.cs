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

    [SerializeField] private Transform ItemsContent;
    [SerializeField] private GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ListItems();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
        ListItems();
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
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
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.Name;
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
                Debug.Log("Mana is used");
                var playerMana = player.GetManaSystem();
                playerMana.AddMana(item.Amount);
                break;
            case Item.ItemType.ExperiencePotion:
                break;
            case Item.ItemType.WoodenSword:
                break;
            case Item.ItemType.IronSword:
                break;
            default:
                break;
        }


        RemoveItem(item);
        ListItems(); // refresh the UI
    }
}
