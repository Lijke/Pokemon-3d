using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    BattleState state;
    public PokemonObject enemyPokemonFight;
    public PokemonObject playerPokemonFight;
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

    public PokemonObject pokemonToheal;
    public InventoryObject itemToHeal;
    public GameObject battleCanvas;
    public bool fighting;
    private int bestMove;
    public int effectiveness;
    private void Awake()
    {
       
    }

    private void Update()
    {
        if(fighting)
        {
            fighting = false;
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }
       

    }
    public IEnumerator SetupBattle()
    {
        battleCanvas.SetActive(true);
        //PLAYERSPAWN OBJECT + UPDATEUI
        var playerPokemon = Instantiate(pokemonInventry.ContainerPokemon[0].item.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        playerPokemonFight = pokemonInventry.ContainerPokemon[0].item;
        playerPokemon.GetComponent<Transform>().position = playerPokemonTransform.position;
        playerHud.SetPlayerHud(pokemonInventry.ContainerPokemon[0].item);
        //ENEMY SPAWN OBJECT + UPDATEUI
        var enemyPokemon = Instantiate(enemyPokemonFight.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        enemyPokemon.GetComponent<Transform>().position = enemyPokemonTransform.position;
        enemyHud.SetPlayerHud(enemyPokemonFight);
        yield return new WaitForSeconds(0f);
        PlayerTurn();
    }
    #region PlayerTurn
    public void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        choseAcctionUi.SetActive(true);

    }
    public void PlayerTurnChoseAcction(string Acction)
    {
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
        choseMovesUi.SetActive(false);
        choseAcctionUi.SetActive(true);
        //enemyTurn
        EnemyChoseMove();

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
        foreach (Transform child in InventoryCanvas.transform)
        {
            child.GetComponentInChildren<Button>().interactable = false;
        }
    }
    public void HealPokemon(int valueToHeal)
    {
        pokemonToheal.currentHealth += valueToHeal;
        if(pokemonToheal.currentHealth > pokemonToheal.maxHealth)
        {
            pokemonToheal.currentHealth = pokemonToheal.maxHealth;
        }
        playerHud.SetPlayerHud(pokemonToheal);
        pokemonInventoryCanvas.SetActive(false);
        InventoryCanvas.SetActive(false);
        foreach (Transform child in pokemonInventoryCanvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in InventoryCanvas.transform)
        {
            child.GetComponentInChildren<Button>().interactable = false;
        }
        //enemyTurn
        EnemyChoseMove();

    }
    public void PlayerTurnRunAway()
    {
        //zrobić player turn :)
    }
    #endregion

    #region EnemyTurn
    public void EnemyChoseMove()
    {
        for (int i = 0; i < enemyPokemonFight.moves.Count; i++)
        {
            var enemyMoveType = enemyPokemonFight.moves[i].moveType;
            var playerPokemonType = playerPokemonFight.type;
            float[][] chart =
                {//                      wat  nor fire  grass
                   /*wat*/ new float[] { 2f, 2f, 2f, 2f },
                   /*nor*/new float[] { 2f, 1f, 2f, 4f },
                   /*fire*/new float[] { 2f, 4f, 1, 1f },
                   /*grass*/new float[] { 2f, 1f, 4f, 1f }
                };
            int row = (int)playerPokemonType;
            int col = (int)enemyMoveType;
            effectiveness = (int)chart[row][col];
            if(effectiveness==4)
            {
                bestMove = i;  
            }
        }
        StartCoroutine(EnemyAttack(enemyPokemonFight.moves[bestMove].baseDamage,bestMove));
    }
    public IEnumerator EnemyAttack(int baseDamage,int moves)
    {
        Debug.Log(baseDamage + "base");
        playerPokemonFight.currentHealth -= baseDamage * effectiveness;
        Debug.Log(baseDamage * effectiveness);
        yield return new WaitForSeconds(1f);
    }
         
    #endregion
}
