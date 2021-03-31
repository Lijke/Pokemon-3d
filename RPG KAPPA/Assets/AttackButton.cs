using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    BattleSystem battleSystem;
    private void Awake()
    {
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
    }
    public void AttackButtonChosen()
    {
        battleSystem.PlayerTurnChoseAcction("Attack");
    }
}
