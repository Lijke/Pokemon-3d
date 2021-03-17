using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hammer : MonoBehaviour
{
    public Sprite resourcesHit;
    InventorySystem invSystem;
    public bool canMining=false;
    private void Start()
    {
        invSystem = GameObject.Find("InventorySystem").GetComponent<InventorySystem>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.tag=="Tree")
        {
            if(canMining)
            {
                resourcesHit = other.gameObject.GetComponent<Tree_Container>().wood;
                invSystem.invetorySlots[0].GetComponent<Image>().sprite = resourcesHit;
                invSystem.invetorySlots[0].GetComponentInChildren<Text>().text += 1;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
           

        }
    }
}
