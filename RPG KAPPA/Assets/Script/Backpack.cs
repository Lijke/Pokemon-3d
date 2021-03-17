using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public List<GameObject> item;
    public GameObject backpackCanvas;
    public GameObject pokemonContainer;
    public Transform startPosToSpawnContainer;

    public void ShowBackpack()
    {
        for (int i = 0; i < item.Count; i++)
        {
            Vector2 spawnPos = new Vector2(startPosToSpawnContainer.GetComponent<RectTransform>().transform.position.x, startPosToSpawnContainer.GetComponent<RectTransform>().transform.position.y - (110 * i * 0.5f));
            GameObject buffor = Instantiate(pokemonContainer, spawnPos, Quaternion.identity, startPosToSpawnContainer);
            var container = buffor.GetComponent<PokemonContainer>();
            container.pokemonText.text = item[i].GetComponent<Item>().itemName;
            container.count = i;
        }
    }
}
