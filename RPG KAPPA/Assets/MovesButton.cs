using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesButton : MonoBehaviour
{
    public MovesObject move;
    public BattleSystem battleSystem;

    public void AttackWithMove()
    {
        var damage = move.baseDamage;
        var name = move.moveName;
        var type = move.moveType;
        battleSystem.PlayerTurnAttack(damage, name, type);
    }
}
