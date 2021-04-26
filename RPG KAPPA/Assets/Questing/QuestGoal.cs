using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;
    public int requiredAmmount;
    public int currentAmmount;
    public string enemyName;
    [SerializeField] GameEvent gameEvent;


    public bool isReached()
    {
       
        return (currentAmmount >= requiredAmmount);
        
    }
    public void Reward()
    {
        Debug.Log("Odpalilo sie ");
        gameEvent?.Invoke();
        
    }
    public void  EnemyKilled(string killedEnemyName)
    {
        if(enemyName==killedEnemyName)
        {
            if (goalType == GoalType.Kill)
                currentAmmount++;
        }
        
    }
}

public enum GoalType
{
    Kill,
    Gathering
}
