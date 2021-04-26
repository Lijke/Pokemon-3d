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
    public bool isReached()
    {
        return (currentAmmount >= requiredAmmount);
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
