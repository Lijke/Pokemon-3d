using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Potion Object", menuName ="InventorySystem/Items/Potions")]
public class PotionObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Potion;
    }
}
