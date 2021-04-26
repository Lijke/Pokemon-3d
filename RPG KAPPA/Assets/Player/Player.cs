using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public PokemonInventory pokemonInventory;
    public List<Quest> quest;
    public string killedEnemyName;
    public void Battle()
    {
        //test
        foreach (var item in quest)
        {
            item.questGoal.EnemyKilled(killedEnemyName);
        }
        if(quest[0].isActive)
        {
            quest[0].questGoal.EnemyKilled(killedEnemyName);
            if(quest[0].questGoal.isReached())
            {
                //dzieje se cos jak sie zsrobi misje
            }
        }
    }


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
