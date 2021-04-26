using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState 
{ START, 
  PLAYERTURN,
   PLAYERTURNCHOSEMOVE,
    ENEMYTURN,
    WON, 
    LOST, 
    HEAL 
}
public class BattleSystem : MonoBehaviour
{
    public Player player;
   [SerializeField] BattleState state;
    public PokemonObject enemyPokemonFight;
    public PokemonObject playerPokemonFight;
    private GameObject playerPokemon;
    private GameObject enemyPokemon;
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

    public Text dialogueText;
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
        playerPokemon = Instantiate(pokemonInventry.ContainerPokemon[0].item.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        playerPokemonFight = pokemonInventry.ContainerPokemon[0].item;
        playerPokemon.GetComponent<Transform>().position = playerPokemonTransform.position;
        playerHud.SetPlayerHud(pokemonInventry.ContainerPokemon[0].item);
        //ENEMY SPAWN OBJECT + UPDATEUI
        enemyPokemon = Instantiate(enemyPokemonFight.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        enemyPokemon.GetComponent<Transform>().position = enemyPokemonTransform.position;
        enemyHud.SetPlayerHud(enemyPokemonFight);
        dialogueText.text = enemyPokemonFight.namePokemon + " is attacking you!";
        yield return new WaitForSeconds(1f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    #region PlayerTurn
    public void PlayerTurn()
    {
        dialogueText.text = "Chose acction";
        state = BattleState.PLAYERTURNCHOSEMOVE;
        choseAcctionUi.SetActive(true);

    }
    public void PlayerTurnChoseAcction(string Acction)
    {
        if(state==BattleState.PLAYERTURNCHOSEMOVE)
        {
            if (Acction == "Attack")
            {
                Acction = "";
                choseAcctionUi.SetActive(false);
                PlayerTurnAttackShowMove();
            }
            else if (Acction == "Heal")
            {
                Acction = "";
                PlayerTurnHeal();
            }
            else if (Acction== "SwitchPokemon")
            {
                Acction = "";
                ChangePokemonInBattle();
            }
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
        var enemyType = enemyPokemonFight.type;
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
        StartCoroutine(enemyHud.SetPlayerHudWhenHit(enemyPokemonFight, enemyPokemonFight.currentHealth, damageDeal));
        state = BattleState.ENEMYTURN;
        enemyPokemonFight.currentHealth -= damageDeal;
        bool isDead=CheckDamage(enemyPokemonFight);
        if(isDead)
        {
            EndBattle(true);
        }
        else
        {
            choseMovesUi.SetActive(false);
            choseAcctionUi.SetActive(true);
            dialogueText.text = "Enemy Turn";
            StartCoroutine(EnemyChoseMove());
        }
     

    }
    public void PlayerTurnHeal()
    {
        pokemonInventoryCanvas.SetActive(true);
        dialogueText.text = "Chose pokemon to heal";
        displayPokemonInventory.CreateDisplayInFight();
    }
    public IEnumerator ChoseItemToHealPokemon()
    {
        dialogueText.text = "Chose item to heal";
        InventoryCanvas.SetActive(true);
        displayItemInventory.CreateDisplayInFight();
        yield return new WaitForSeconds(1);
        foreach (Transform child in InventoryCanvas.transform)
        {
            if (child.GetComponentInChildren<Button>() != null)
                child.GetComponentInChildren<Button>().interactable = true;
           
        }

        
    }
    public void HealPokemon(int valueToHeal)
    {
        Debug.Log("OK");
        pokemonToheal.currentHealth += valueToHeal;
        if(pokemonToheal.currentHealth > pokemonToheal.maxHealth)
        {
            pokemonToheal.currentHealth = pokemonToheal.maxHealth;
        }
        if(pokemonToheal.namePokemon==playerPokemonFight.namePokemon)
        {
            playerHud.SetPlayerHud(pokemonToheal);
        }
        pokemonInventoryCanvas.SetActive(false);
        InventoryCanvas.SetActive(false);
        foreach (Transform child in pokemonInventoryCanvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in InventoryCanvas.transform)
        {
            if(child.GetComponentInChildren<Button>() != null)  
            child.GetComponentInChildren<Button>().interactable = false;
        }
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyChoseMove());

    }
    public void PlayerTurnRunAway()
    {
        //zrobić run away;
    }

    public void ChangePokemonInBattle()
    {
        battleCanvas.SetActive(true);
        displayPokemonInventory.CreateDisplayInFightWhenChangePokemon();
        playerHud.SetPlayerHud(playerPokemonFight);
        EnemyChoseMove();
        
    }
    #endregion

    #region EnemyTurn
    public IEnumerator EnemyChoseMove()
    {
        if(state==BattleState.ENEMYTURN)
        {
            yield return new WaitForSeconds(1f);
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
                if (effectiveness == 4)
                {
                    bestMove = i;
                }
            }
            StartCoroutine(EnemyAttack(enemyPokemonFight.moves[bestMove].baseDamage, bestMove));
        }

    }
    public IEnumerator EnemyAttack(int baseDamage,int moves)
    {
        dialogueText.text = "Enemy pokemon attack!";
        yield return new WaitForSeconds(1f);
        var damage= baseDamage * effectiveness;
        StartCoroutine(playerHud.SetPlayerHudWhenHit(playerPokemonFight, playerPokemonFight.currentHealth, damage));
        playerPokemonFight.currentHealth -= damage;
        bool isDead = CheckDamage(playerPokemonFight);
        if(isDead)
        {
            ShowPlayerPokemon();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();

        }
        
    }

    #endregion
    #region EndBattle
    public void EndBattle(bool isWin)
    {
        if(isWin)
        {
            Debug.Log("IsWin");
            dialogueText.text = "You lose battle!";
            choseMovesUi.SetActive(false);
            choseAcctionUi.SetActive(true);
            battleCanvas.SetActive(false);
            Destroy(playerPokemon);
            Destroy(enemyPokemon);
            player.QuestCheckAfterBattle(enemyPokemonFight.namePokemon);

        }
        else
        {
            Debug.Log("lose");
            dialogueText.text = "You lose battle!";
            choseMovesUi.SetActive(false);
            choseAcctionUi.SetActive(true);
            battleCanvas.SetActive(false);
            Destroy(playerPokemon);
            Destroy(enemyPokemon);
        }
        
    }
    #endregion
    public void ShowPlayerPokemon()
    {
        bool isAllPokemonDead = false;
        for (int i = 0; i < pokemonInventry.ContainerPokemon.Count; i++)
        {
            if(pokemonInventry.ContainerPokemon[i].item.currentHealth>0)
            {
                isAllPokemonDead = false;
                break;
            }
            else
            {
                isAllPokemonDead = true;
            }
        }
        if(isAllPokemonDead)
        {
            EndBattle(true);
        }
        else
        {
            dialogueText.text = "Chose pokemon to fight";
            pokemonInventoryCanvas.SetActive(true);
            displayPokemonInventory.CreateDisplayInFightWhenChangePokemon();
        }
        

    }
    public void ClearAllBeforeRespawnOtherPokemon(int pokemonIndexInContainer)
    {
        Destroy(playerPokemon);
        playerPokemon = Instantiate(pokemonInventry.ContainerPokemon[pokemonIndexInContainer].item.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        playerPokemonFight = pokemonInventry.ContainerPokemon[pokemonIndexInContainer].item;
        playerPokemon.GetComponent<Transform>().position = playerPokemonTransform.position;
        playerHud.SetPlayerHud(pokemonInventry.ContainerPokemon[pokemonIndexInContainer].item);
        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }
    public bool CheckDamage(PokemonObject pokemonObject)
    {
        if (pokemonObject.currentHealth < 0)
            return true;
        else
            return false;
    }
}
