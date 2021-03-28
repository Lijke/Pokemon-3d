using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Invetory", menuName = "Inventory System/Pokemon Inventory")]
public class PokemonInventory : ScriptableObject
{
    public List<InventoryPokemonSlot> ContainerPokemon = new List<InventoryPokemonSlot>();

    public void AddPokemon(PokemonObject _pokemon)
    {
        bool hasPokemon = false;
        for (int i = 0; i < ContainerPokemon.Count; i++)
        {
            if (ContainerPokemon[i].item == _pokemon)
            {
                //dodaj golda bo juz złapałeś tego pokemona
                hasPokemon = true;
                break;
            }
            else
            {
                ContainerPokemon.Add(new InventoryPokemonSlot(_pokemon));
            }
        }
    }
}

[System.Serializable]
public class InventoryPokemonSlot
{
    public PokemonObject item;

    public InventoryPokemonSlot(PokemonObject _item)
    {
        item = _item;
    }

}