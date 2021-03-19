using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { START,PLAYERTURN,ENEMYTURN,WON,LOST}
public class BattleManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    MouseLook mouseLook;
    [SerializeField] private Backpack backpack;
    public BattleState state;

    public List<GameObject> playerPrefab;
    public List<GameObject> enemyPrefab;

    public Transform playerTransform;
    public Transform enemyTransform;

    Unit playerUnit;
    Unit enemyUnit;

    //ui
    public Text dialogueText;
    public BattleHud playerHud;
    public BattleHud enemyHud;

    public GameObject combatButtonUi;
    public GameObject moveUi;
    public GameObject battleCanvas;
    public GameObject backpackCanvas;
    public GameObject pokemonCanvas;

    public GameObject pokemonBackackContainer;
    public Transform startPosPokemonBackpack;
    //choseMove
    public List<Text> button;
    public List<Text> movesPP;
    public List<Moves> moves;
    //Chose Pokemon To Interact
    public int chosenPokemon;
    public int chosenItem;
    public int howManyHpAdd;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        mouseLook = GameObject.Find("Player").GetComponentInChildren<MouseLook>();
        moveUi.SetActive(false);
    }
    void Start()
    {
        state = BattleState.START;
        playerMovement.canMove = false;
        mouseLook.canLookAround = false;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab[0], playerTransform);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGo =Instantiate(enemyPrefab[0], enemyTransform);
        enemyUnit = enemyGo.GetComponent<Unit>();

        playerHud.SetHud(playerUnit);
        enemyHud.SetHud(enemyUnit);
        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(2);
        PlayerTurn();
    }
    public void PlayerTurn()
    {
        dialogueText.text = "DO STH";
    }
    public void OnAttackButtonAttack()
    {
        combatButtonUi.SetActive(false);
        moveUi.SetActive(true);
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(ChoseMove());
    }
    public void OnAttackBackpack()
    {
        combatButtonUi.SetActive(false);
        moveUi.SetActive(false);
        pokemonCanvas.SetActive(true);
        ShowPokemonInBattle();

        //backpackCanvas.SetActive(true);
        //backpack.ShowBackpack();

    }
    public void ShowPokemonInBattle()
    {
        
            for (int i = 0; i < playerPrefab.Count; i++)
            {
                Vector2 spawnPos = new Vector2(startPosPokemonBackpack.GetComponent<RectTransform>().transform.position.x,
                    startPosPokemonBackpack.GetComponent<RectTransform>().transform.position.y - (110 * i * 0.5f));
                GameObject buffor = Instantiate(pokemonBackackContainer, spawnPos, Quaternion.identity, startPosPokemonBackpack);
                var container = buffor.GetComponent<PokemonContainer>();
                container.pokemonText.text = playerPrefab[i].GetComponent<Unit>().unitName;
                // dodać slider i image
                container.count = i;
            }
            
    }
    public void ChosePokemonToHeal()
    {
        pokemonCanvas.SetActive(false);
        backpackCanvas.SetActive(true);
        backpack.ShowBackpack();

    }
    public void HealPokemon()
    {
        Debug.Log(playerPrefab[chosenPokemon].GetComponent<Unit>().currentHp);
        playerPrefab[chosenPokemon].GetComponent<Unit>().currentHp += howManyHpAdd;
        backpackCanvas.SetActive(false);
        pokemonCanvas.SetActive(false);
        //jutro dorobić zmniejszanie countu itemów, reset ui pokemonów aby pokazywało normalnie hp, zrobić state= itd... :)
    }
    public IEnumerator ChoseMove()
    {
 
        for (int i = 0; i < 4; i++)
        {
            moves.Add(playerUnit.pokemonSpells[i]);
        }
        for (int i = 0; i < 4; i++)
        {
            button[i].text = moves[i].nameMove;
            movesPP[i].text = moves[i].currentPowerPoint.ToString();
        } 

        yield return new WaitForSeconds(2);
    }
    public void DoDamage(int playerPokemonSpell)
    {
        int enemyPokemonType = (int)enemyUnit.pokemontype;
        float[][] chart =
               {//                      nor  fir wat  grass
                   /*nor*/ new float[] { 2f, 2f, 2f, 2f },
                   /*fire*/new float[] { 2f, 1f, 2f, 4f },
                   /*water*/new float[] { 2f, 4f, 1, 1f },
                   /*grass*/new float[] { 2f, 1f, 4f, 1f }
                };
        int effectiveness = (int)chart[playerPokemonSpell][enemyPokemonType];
        Debug.Log(effectiveness);
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHud.SetHp(enemyUnit.currentHp);
        dialogueText.text = "attack is sucneuf euafnaefnmaf";
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + "Attack";
        yield return new WaitForSeconds(2);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHud.SetHp(playerUnit.currentHp);
        yield return new WaitForSeconds(2);
        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    void EndBattle()
    {
        if(state== BattleState.WON)
        {
            dialogueText.text = "WON";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = " You were defated";
        }
    }
}
