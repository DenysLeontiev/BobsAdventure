using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDeletionButton : MonoBehaviour,IPointerDownHandler
{
    private Item itemToDelete;

    private float dclick_threshold = 0.25f;
    private double timerdclick = 0;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(1)) // double right click to remove item
        {
            if ((Time.time - timerdclick) <= dclick_threshold)
            {
                if(itemToDelete != null)
                {
                    Debug.Log("itemToDelete: " + itemToDelete.ItemPrefab);
                    SpawnItemInWorld(itemToDelete);
                }
                else
                {
                    Debug.LogError("Item is null");
                }
            }

            timerdclick = Time.time;
        }
    }

    public void SetItem(Item itemToDelete)
    { 
        this.itemToDelete = itemToDelete; 
    }

    private void SpawnItemInWorld(Item itemToSpawn)
    {
        float minDistance = 1.0f;
        float maxDistance = 2.0f;

        var spawnedItem = Instantiate(itemToSpawn.ItemPrefab);

        Transform player = PlayerController.Instance.transform;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector3 randomPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0) * randomDistance;

        spawnedItem.transform.position = randomPosition;

        InventoryManager.Instance.RemoveItem(itemToDelete);
        InventoryManager.Instance.ListItems(); //refresh
    }
}
