using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePokemonInBattleButton : MonoBehaviour
{
    BattleSystem battleSystem;
    private void Awake()
    {
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
    }

    public void ChangePokemon()
    {
        battleSystem.PlayerTurnChoseAcction("SwitchPokemon");
    }
}
