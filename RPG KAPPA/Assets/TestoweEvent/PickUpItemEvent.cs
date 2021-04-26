using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemEvent : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    public ItemObject item;
    public int ammount;
    InventoryObject inventoryObject;

    private void Awake()
    {
        inventoryObject = GameObject.Find("Player").GetComponent<Player>().inventory;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            gameEvent?.Invoke();
        }
    }

    public void AddItem()
    {
        inventoryObject.AddItem(item, ammount);
        Destroy(gameObject);
    }

}
