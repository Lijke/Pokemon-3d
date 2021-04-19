using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = " New Gold Object", menuName = "Inventory System/Items/Gold")]
public class GoldObject : ItemObject
{

    private void Awake()
    {
        type = ItemType.Gold ;
    }
}
