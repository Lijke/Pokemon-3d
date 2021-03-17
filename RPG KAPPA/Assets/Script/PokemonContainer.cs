using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PokemonContainer : MonoBehaviour
{
    public Text pokemonText;
    public Slider pokemonSlider;
    public Button pokemonButton;
    BattleManager bm;
    public int count;
    private void Start()
    {
        bm = GameObject.Find("BattleSystem").GetComponent<BattleManager>();
    }
    public void HealPokemon()
    {
        
    }
    
}
