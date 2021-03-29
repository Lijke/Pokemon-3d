using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Normal Pokemon", menuName = "Pokemon/Normal Pokemon")]
public class NormalPokemon : PokemonObject
{
    public List<MovesObject> moves = new List<MovesObject>();
    public void Awake()
    {
        type = PokemonType.Normal;
    }
}
