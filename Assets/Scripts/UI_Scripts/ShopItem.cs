using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ShopItemSO shopItem;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;

    private CoinSystem playerCoinSystem;

    private void Start()
    {
        priceText.text = shopItem.Price.ToString();
        nameText.text = shopItem.item.Name;

        playerCoinSystem = PlayerController.Instance.GetComponent<CoinSystem>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(shopItem != null)
        {
            int currentCoinsAmount = InventoryManager.Instance.GetAmountOfCoins();
            Debug.Log("currentCoinsAmount: " + currentCoinsAmount);
            int itemPrice = shopItem.Price;

            if(playerCoinSystem.TakeCoins(itemPrice))
                InventoryManager.Instance.AddItem(shopItem.item);
            else
                Debug.Log("Not enough money");

            InventoryManager.Instance.ListItems();
        }
    }
}
