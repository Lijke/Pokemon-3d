using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public List<PokemonObject> enemyPokemon;
    BattleSystem battleSystem;

    private void Awake()
    {
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            var random = Random.Range(0, enemyPokemon.Count);
            battleSystem.enemyPokemonFight=enemyPokemon[random];
            battleSystem.fighting = true;
        }
    }
}
