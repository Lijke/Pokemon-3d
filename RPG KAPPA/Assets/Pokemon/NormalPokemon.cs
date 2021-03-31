using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Normal Pokemon", menuName = "Pokemon/Normal Pokemon")]
public class NormalPokemon : PokemonObject
{
    public void Awake()
    {
        type = PokemonType.Normal;
    }
}
