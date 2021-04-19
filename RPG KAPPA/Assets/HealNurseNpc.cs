using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealNurseNpc : MonoBehaviour
{
    public string senteces;
    public Text dialogueText;
    public GameObject dialogueCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var pokemon = other.GetComponent<Player>().pokemonInventory;
            for (int i = 0; i < pokemon.ContainerPokemon.Count; i++)
            {
                pokemon.ContainerPokemon[i].item.currentHealth = pokemon.ContainerPokemon[i].item.maxHealth;
            }
            StartCoroutine(AfterHealing());
        }
       
           
    }
    public IEnumerator AfterHealing()
    {
        dialogueCanvas.SetActive(true);
        foreach (char item in senteces)
        {
            dialogueText.text += item;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        dialogueCanvas.SetActive(false);
    }
}
