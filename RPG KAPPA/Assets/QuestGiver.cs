using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public Player player;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");   
        if(other.gameObject.tag=="Player")
        player.quest.Add(quest);
    }
}
