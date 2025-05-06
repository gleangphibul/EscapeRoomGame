using UnityEngine;
using System.Collections;
using TMPro;

public class DeskItemController : Interactable
{
    [Header("Panel Management")]
    public PanelManager panelManager;
    private bool isUnlocked = false;
    public GameObject lockedPanel;

    [Header("Required Item")]
    public int requiredItemID = 1;
    public string requiredItemName = "holepaper";

    private InventoryController inventoryController;

    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }

    void Update()
    {
    }
    public override void Interact()
    {

        if (isUnlocked)
        {
            panelManager.ShowFinalPanelOnly();
        }
        else {
            OpenLockedPanel();
            GameObject requiredItem = FindRequiredItemInInventory();
            if (requiredItem != null) {
                // Player has the item, proceed to next panel
                panelManager.MoveToNextPanel();
                RemoveItemFromInventory(requiredItem);
                isUnlocked = true;
            } else {
                // Player doesn't have the required item
            }
        }
    }

    private void OpenLockedPanel()
    {
        lockedPanel.SetActive(!lockedPanel.activeSelf);
    }

    private GameObject FindRequiredItemInInventory()
    {
        // Check all slots in the inventory panel
        foreach (Transform slotTransform in inventoryController.inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null && item.ID == requiredItemID)
                {
                    return slot.currentItem;
                }
            }
        }
        
        return null;
    }

    private void RemoveItemFromInventory(GameObject itemObject)
    {
        // Find the slot containing the item
        Slot itemSlot = itemObject.GetComponentInParent<Slot>();
        
        if (itemSlot != null)
        {
            // Remove the reference to the item
            itemSlot.currentItem = null;
            
            // Destroy the item game object
            Destroy(itemObject);
        }
    }
}