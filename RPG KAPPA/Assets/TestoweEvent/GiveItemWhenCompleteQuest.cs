using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItemWhenCompleteQuest : MonoBehaviour
{
    public ItemObject item;
    public int ammount;
    InventoryObject inventoryObject;

    private void Awake()
    {
        inventoryObject = GameObject.Find("Player").GetComponent<Player>().inventory;
    }


    public void AddItem()
    {
        inventoryObject.AddItem(item, ammount);
        Destroy(gameObject);
    }
}
