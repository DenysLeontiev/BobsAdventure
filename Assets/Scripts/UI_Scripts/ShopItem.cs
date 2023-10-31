using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Item shopItem;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Cliked on " + shopItem);
    }
}
