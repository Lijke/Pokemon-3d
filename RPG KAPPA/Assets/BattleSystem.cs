using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    BattleState state;
    public Transform playerPokemonTransform;
    public Transform enemyPokemonTransform;

    public PokemonInventory pokemonInventry;
    public BattleHud playerHud;
    public BattleHud enemyHud;

    public GameObject attackButtonUi;
    public GameObject healButtonUi;
    public GameObject choseAcctionUi;
    public GameObject choseMovesUi;
    public List<GameObject> movesButton;
    private void Awake()
    {
       
    }

    private void Start()
    {
        state = BattleState.START;
        SetupBattle();

    }
    public IEnumerator SetupBattle()
    {
        //PLAYERSPAWN OBJECT + UPDATEUI
        var playerPokemon = Instantiate(pokemonInventry.ContainerPokemon[0].item.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        playerPokemon.GetComponent<Transform>().position = playerPokemonTransform.position;
        playerHud.SetPlayerHud(pokemonInventry.ContainerPokemon[0].item);
        //ENEMY SPAWN OBJECT + UPDATEUI
        var enemyPokemon = Instantiate(pokemonInventry.ContainerPokemon[1].item.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        enemyPokemon.GetComponent<Transform>().position = enemyPokemonTransform.position;
        enemyHud.SetPlayerHud(pokemonInventry.ContainerPokemon[1].item);
        yield return new WaitForSeconds(0f);
        PlayerTurn();
    }
    public void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        choseAcctionUi.SetActive(true);

    }
    public void PlayerTurnChoseAcction(string Acction)
    {
        Debug.Log(Acction);
        if(Acction=="Attack")
        {
            choseAcctionUi.SetActive(false);
            PlayerTurnAttack();
        }
        else if (Acction=="Heal")
        {
            PlayerTurnHeal();
        }
    }
    public void PlayerTurnAttack()
    {
        choseMovesUi.SetActive(true);
        var lenghtMoves = pokemonInventry.ContainerPokemon[0].item.moves.Count;
        for (int i = 0; i < lenghtMoves; i++)
        {
            var currentPokemonMoves = pokemonInventry.ContainerPokemon[0].item.moves[i];
            movesButton[i].GetComponentInChildren<Text>().text = currentPokemonMoves.moveName;
        }
        for (int i = lenghtMoves; i < 4; i++)
        {
            movesButton[i].SetActive(false);
        }

        

    }
    public void PlayerTurnHeal()
    {

    }
    public void PlayerTurnRunAway()
    {

    }
}
