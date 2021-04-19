using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ObjectState
{
  Heal,
  ChangePokemon
}
public class HealButton : MonoBehaviour
{
    public BattleSystem battleSystem;
    public PokemonObject pokemon;
    public PokemonObject pokemonSwitch;
    public Button button;
    public ObjectState state;
    private int IndexOfPokemonInContainer;
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
        IndexOfPokemonInContainer = i;

    }
    public void ChoseItemToHealPokemon()
    {
        if(state==ObjectState.Heal)
        {
            battleSystem.pokemonToheal = pokemon;

            StartCoroutine(battleSystem.ChoseItemToHealPokemon());
        }
    }
    public void ButtonWhenSwitchPokemon()
    {
        if(state==ObjectState.ChangePokemon)
        {
            Debug.Log("OPALA SIE !");
            button.GetComponentInChildren<Text>().text = "Fight!";
            if (pokemon.currentHealth <= 0)
            {
                Debug.Log("You can't use this pokemon");
            }
            else
            {
                battleSystem.playerPokemonFight = pokemonSwitch;
                battleSystem.ClearAllBeforeRespawnOtherPokemon(IndexOfPokemonInContainer);
                battleSystem.pokemonInventoryCanvas.SetActive(false);
            }
        }
        
    }
    public void ChangeState(string Acction)
    {
        if (Acction == "Heal")
            state = ObjectState.Heal;
        else if (Acction == "Switch")
            state = ObjectState.ChangePokemon;
              
    }
}
