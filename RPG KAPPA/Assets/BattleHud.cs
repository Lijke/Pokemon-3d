using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleHud : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI name;
    public TextMeshProUGUI level;

    public void SetPlayerHud(PokemonObject pokemonObject)
    {
        slider.value =(float) pokemonObject.currentHealth /(float) pokemonObject.maxHealth;
        name.text = pokemonObject.namePokemon;
        //dodac level do pokemonObject :)
        // level.text = pokemonObject.text;
    }
}
