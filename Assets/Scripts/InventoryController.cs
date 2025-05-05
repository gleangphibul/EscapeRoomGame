using UnityEngine;
using System.Collections.Generic; // Needed for List<T>

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    public List<Item> inventoryItems = new List<Item>(); // List of all items in inventory

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                GameObject itemObject = Instantiate(itemPrefabs[i], slot.transform);
                itemObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Item item = itemObject.GetComponent<Item>(); // Ensure the Item script is attached
                inventoryItems.Add(item); // Add to inventoryItems list
                slot.currentItem = itemObject;
            }
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItemObject = Instantiate(itemPrefab, slotTransform);
                newItemObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Item newItem = newItemObject.GetComponent<Item>(); // Ensure the Item script is attached
                inventoryItems.Add(newItem); // Add to inventoryItems list
                slot.currentItem = newItemObject;
                return true;
            }
        }

        Debug.Log("Inventory is full");
        return false;
    }

    public bool HasKeyForObject(string requiredItemId)
    {
        foreach (Item item in inventoryItems)
        {
            if (item.isKey && item.unlocksObjectId == requiredItemId)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].itemName == itemName)
            {
                inventoryItems.RemoveAt(i); // Remove the item
                return;
            }
        }
    }
}
