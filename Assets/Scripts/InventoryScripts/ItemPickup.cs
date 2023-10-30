using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item itemToPickUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        Debug.Log(itemToPickUp.Quantity);
        InventoryManager.Instance.AddItem(itemToPickUp);
        Destroy(gameObject);
    }
}
