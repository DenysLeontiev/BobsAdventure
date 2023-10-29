using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int Id;
    public string Name;
    public string Description;
    public int Amount;
    public Sprite Icon; 
    public ItemType Type;
    public GameObject ItemPrefab;

    public enum ItemType
    { 
        HealthPotion,
        ManaPotion,
        ExperiencePotion,
        WoodenSword,
        IronSword
    }

}
