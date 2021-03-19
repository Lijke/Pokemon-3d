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
    Backpack bp;
    public int count;
    private void Start()
    {
        bm = GameObject.Find("BattleSystem").GetComponent<BattleManager>();
        bp = GameObject.Find("Backpack").GetComponent<Backpack>();
    }
    public void ChosePokemonToHeal()
    {
        bm.chosenPokemon = count;
        bm.ChosePokemonToHeal();
    }
   public void HealPokemon()
    {
        bm.chosenItem = count;
        bm.howManyHpAdd += bp.item[count].GetComponent<Item>().healthGain;
        bm.HealPokemon();
    }
    
}
