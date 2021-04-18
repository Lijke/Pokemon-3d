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
    BattleSystem battleSystem;
    private void Start()
    {
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
    }

    public void SetPlayerHud(PokemonObject pokemonObject)
    {
        slider.value =(float) pokemonObject.currentHealth /(float) pokemonObject.maxHealth;
        name.text = pokemonObject.namePokemon;
        //dodac level do pokemonObject :)
        // level.text = pokemonObject.text;
    }
    public IEnumerator SetPlayerHudWhenHit(PokemonObject pokemonObject, int currentHp,int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            currentHp -= 1;
            slider.value = (float)currentHp /(float)pokemonObject.maxHealth;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
