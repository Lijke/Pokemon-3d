using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pokemonType
{
    Water,
    Fire,
    Normal,
    Grass

}
public class Unit : MonoBehaviour
{
    public pokemonType pokemontype;
    public string unitName;
    public int unityLevel;

    public int damage;

    public int maxHp;
    public int currentHp;
    public List<Moves> pokemonSpells;
    public bool TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
            return true;
        else
            return false;
    }
}
