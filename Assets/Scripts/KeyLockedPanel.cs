using UnityEngine;
using TMPro;

public class KeyLockedPanel : Interactable
{
    [Header("Key Settings")]
    public int requiredKeyID; // The ID of the key item needed to unlock this panel
    public string keyName; // Optional: The name of the key for debugging or UI display

    [Header("UI Elements")]
    public GameObject lockedPanel;

    [Header("Panel Manager")]
    public PanelManager panelManager;
    public int requiredItemPanelIndex = 1;

    private bool isUnlocked = false;
    private InventoryController inventoryController;

    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }

    public override void Interact()
    {
        if (isUnlocked)
        {
            Debug.Log("Panel already unlocked. Showing final panel.");
            panelManager.ShowFinalPanelOnly();
        }
        else
        {
            OpenLockedPanel();
            TryUnlockWithKey();
        }
    }

    private void OpenLockedPanel()
    {
        lockedPanel.SetActive(!lockedPanel.activeSelf);
        
    }

    private void TryUnlockWithKey()
    {
        Debug.Log("Checking for key with ID: " + requiredKeyID);
        
        // Find the key in inventory
        GameObject keyItem = FindKeyInInventory();
        
        if (keyItem != null)
        {
            Debug.Log("Found key: " + keyName);
            
            // Unlock panel
            isUnlocked = true;
            // Remove key from inventory
            RemoveKeyFromInventory(keyItem);
            // Move to next panel
            panelManager.MoveToNextPanel();
        }
        else
        {
            Debug.Log("Key not found in inventory");
        }
    }

    private GameObject FindKeyInInventory()
    {
        // Check all slots in the inventory panel
        foreach (Transform slotTransform in inventoryController.inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null && item.ID == requiredKeyID)
                {
                    return slot.currentItem;
                }
            }
        }
        
        return null;
    }

    private void RemoveKeyFromInventory(GameObject keyItem)
    {
        // Find the slot containing the key
        Slot keySlot = keyItem.GetComponentInParent<Slot>();
        
        if (keySlot != null)
        {
            // Remove the reference to the key item
            keySlot.currentItem = null;
            
            // Destroy the key game object
            Destroy(keyItem);
            
            Debug.Log("Key removed from inventory");
        }
    }

    private void ShowFeedback(bool show)
    {
        if (lockedPanel != null)
        {
            lockedPanel.SetActive(show);
        }
    }
}