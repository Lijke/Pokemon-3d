using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PokemonType
{
    Water,
    Normal,
    Fire,
    Grass,
    Fly,
    Ghost
}

public abstract class PokemonObject : ScriptableObject
{
    public GameObject prefab;
    public GameObject prefab_pokemon;
    public PokemonType type;
    public string namePokemon;
    public int currentHealth;
    public int maxHealth;

    
}
