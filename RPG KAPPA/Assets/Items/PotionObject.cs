﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = " New Food Object", menuName = "Inventory System/Items/Food")]
public class PotionObject : ItemObject
{
    public int restoreHealthValue;
    private void Awake()
    {
        type = ItemType.Food;
    }
}
