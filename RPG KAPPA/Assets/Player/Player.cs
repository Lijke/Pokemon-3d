using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public PokemonInventory pokemonInventory;
    public List<Quest> quest;
    public void QuestCheckAfterBattle(string killedEnemyName)
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
        foreach (var item in quest)
        {
            if(item.questGoal.isReached())
            {
                item.questGoal.Reward();
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
