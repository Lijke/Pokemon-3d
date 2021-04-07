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

    public GameObject pokemonInventoryCanvas;
    public GameObject InventoryCanvas;
    public DisplayPokemonInventory displayPokemonInventory;
    public DisplayInventory displayItemInventory;
    private void Awake()
    {
       
    }

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

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
            PlayerTurnAttackShowMove();
        }
        else if (Acction=="Heal")
        {
            PlayerTurnHeal();
        }
    }
    public void PlayerTurnAttackShowMove()
    {
        choseMovesUi.SetActive(true);
        var lenghtMoves = pokemonInventry.ContainerPokemon[0].item.moves.Count;
        for (int i = 0; i < lenghtMoves; i++)
        {
            var currentPokemonMoves = pokemonInventry.ContainerPokemon[0].item.moves[i];
            movesButton[i].GetComponent<MovesButton>().move = currentPokemonMoves;
            movesButton[i].GetComponentInChildren<Text>().text = currentPokemonMoves.moveName;
        }
        for (int i = lenghtMoves; i < 4; i++)
        {
            movesButton[i].SetActive(false);
        }
    }
    public void PlayerTurnAttack(int damageMove, string nameMove, MoveType moveType)
    {
        var playerType = moveType;
        var enemyType = pokemonInventry.ContainerPokemon[1].item.type;
        float[][] chart =
                {//                      wat  nor fire  grass
                   /*wat*/ new float[] { 2f, 2f, 2f, 2f },
                   /*nor*/new float[] { 2f, 1f, 2f, 4f },
                   /*fire*/new float[] { 2f, 4f, 1, 1f },
                   /*grass*/new float[] { 2f, 1f, 4f, 1f }
                };
        int row = (int)playerType;
        int col = (int)enemyType;
        int effectiveness = (int)chart[row][col];
        int damageDeal = damageMove * effectiveness;
        var enemyPokemon = pokemonInventry.ContainerPokemon[1].item;
        enemyHud.SetPlayerHud(enemyPokemon);
        enemyPokemon.currentHealth -= damageDeal;


    }
    public void PlayerTurnHeal()
    {
        pokemonInventoryCanvas.SetActive(true);
        displayPokemonInventory.CreateDisplayInFight();
    }
    public void ChoseItemToHealPokemon()
    {
        InventoryCanvas.SetActive(true);
        displayItemInventory.CreateDisplayInFight();
    }
    public void PlayerTurnRunAway()
    {

    }
}
