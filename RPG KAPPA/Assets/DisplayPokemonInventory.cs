using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class DisplayPokemonInventory : MonoBehaviour
{
    public PokemonInventory pokemonInventry;
    public int X_START;
    public int Y_START;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    Dictionary<InventoryPokemonSlot, GameObject> itemsDisplay = new Dictionary<InventoryPokemonSlot, GameObject>();
    void Start()
    {
       // CreateDisplay();
        
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < pokemonInventry.ContainerPokemon.Count; i++)
        {
            var obj = Instantiate(pokemonInventry.ContainerPokemon[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            Destroy(obj.GetComponentInChildren<Button>().gameObject);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = pokemonInventry.ContainerPokemon[i].item.namePokemon;
            obj.GetComponentInChildren<Slider>().value = (float)pokemonInventry.ContainerPokemon[i].item.currentHealth /(float) pokemonInventry.ContainerPokemon[i].item.maxHealth;
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
    public void CreateDisplayInFight()
    {
        for (int i = 0; i < pokemonInventry.ContainerPokemon.Count; i++)
        {
            var obj = Instantiate(pokemonInventry.ContainerPokemon[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponentInChildren<HealButton>().ChangeState("Heal");
            obj.GetComponentInChildren<HealButton>().AssignPokemonToButton(i);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = pokemonInventry.ContainerPokemon[i].item.namePokemon;
            obj.GetComponentInChildren<Slider>().value = (float)pokemonInventry.ContainerPokemon[i].item.currentHealth / (float)pokemonInventry.ContainerPokemon[i].item.maxHealth;
        }
    }
    public void CreateDisplayInFightWhenChangePokemon()
    {
        for (int i = 0; i < pokemonInventry.ContainerPokemon.Count; i++)
        {
            var obj = Instantiate(pokemonInventry.ContainerPokemon[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponentInChildren<HealButton>().ChangeState("Switch");
            obj.GetComponentInChildren<HealButton>().button.GetComponentInChildren<Text>().text = "Switch Pokemon";
            obj.GetComponentInChildren<HealButton>().AssignPokemonToButton(i);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = pokemonInventry.ContainerPokemon[i].item.namePokemon;
            obj.GetComponentInChildren<Slider>().value = (float)pokemonInventry.ContainerPokemon[i].item.currentHealth / (float)pokemonInventry.ContainerPokemon[i].item.maxHealth;
        }
    }

}
