using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public Transform playerPokemonTransform;
    public Transform enemyPokemonTransform;

    public PokemonInventory pokemonInventry;
    public BattleHud playerHud;
    public BattleHud enemyHud;
    private void Awake()
    {
       
    }

    private void Start()
    {
        //PLAYERSPAWN OBJECT + UPDATEUI
        var playerPokemon = Instantiate(pokemonInventry.ContainerPokemon[0].item.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        playerPokemon.GetComponent<Transform>().position = playerPokemonTransform.position;
        playerHud.SetPlayerHud(pokemonInventry.ContainerPokemon[0].item);
        //ENEMY SPAWN OBJECT + UPDATEUI
        var enemyPokemon = Instantiate(pokemonInventry.ContainerPokemon[1].item.prefab_pokemon, Vector3.zero, Quaternion.identity, transform);
        enemyPokemon.GetComponent<Transform>().position = enemyPokemonTransform.position;
        enemyHud.SetPlayerHud(pokemonInventry.ContainerPokemon[1].item);

    }
}
