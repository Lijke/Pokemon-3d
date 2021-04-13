using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    public ItemObject potionObject;
    public InventoryObject inventoryObject;
    BattleSystem battleSystem;
    private int index;
    private void Start()
    {
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => HealPokemon());
    }

    public void AssignPotionToButton(int i)
    {
        index = i;
        Debug.Log(i);
        potionObject = inventoryObject.Container[i].item;
    }
    public void HealPokemon()
    {
        if(inventoryObject.Container[index].amount>0)
        {
            inventoryObject.Container[index].amount -= 1;
            battleSystem.HealPokemon(potionObject.restoreHealthValue);
        }
        else
        {
            //nie mozesz uzyc blabla
        }
        
    }
}

