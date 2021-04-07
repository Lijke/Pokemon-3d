using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemObject potionObject;
    public InventoryObject inventoryObject;
  
    public void AssignPotionToButton(int i)
    {
        potionObject = inventoryObject.Container[i].item;
    }
    public void HealPokemon()
    {
        //DOrobić leczenie pokemona, ilosc itemow,update ui itd itp...
    }
}

