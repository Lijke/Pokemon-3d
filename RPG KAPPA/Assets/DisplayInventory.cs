using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    public Transform goldTransform;

    Dictionary<InventorySlot, GameObject> itemsDisplay = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        CreateDisplay();
    }
    void Update()
    {
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if(itemsDisplay.ContainsKey(inventory.Container[i]))
            {
                itemsDisplay[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplay.Add(inventory.Container[i], obj);
            }
        }
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<Button>().interactable = false;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplay.Add(inventory.Container[i], obj);
        }
        var GoldObject = Instantiate(inventory.goldContainer.item.prefab, goldTransform.position, Quaternion.identity, transform);
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
    public void CreateDisplayInFight()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = inventory.Container[i].item.prefab;
            obj.GetComponentInChildren<ItemButton>().AssignPotionToButton(i);
        }
    }
}
