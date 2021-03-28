using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public PokemonInventory pokemonInventory;
    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponentInParent<Item>();
        if(item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
}
