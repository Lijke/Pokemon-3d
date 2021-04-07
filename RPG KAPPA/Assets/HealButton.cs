using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButton : MonoBehaviour
{
    public BattleSystem battleSystem;
    public PokemonObject pokemon;
    private void Awake()
    {
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
    }
    public void AttackButtonChosen()
    {
        battleSystem.PlayerTurnChoseAcction("Heal");
    }
    public void AssignPokemonToButton(int i)
    {
        pokemon = battleSystem.pokemonInventry.ContainerPokemon[i].item;
    }
    public void ChoseItemToHealPokemon()
    {
        battleSystem.ChoseItemToHealPokemon();
    }
}
