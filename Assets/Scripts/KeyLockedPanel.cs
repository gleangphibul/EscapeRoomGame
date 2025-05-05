using UnityEngine;
using TMPro;

public class KeyLockedPanel : Interactable
{
    [Header("Item Requirements")]
    public string requiredItemId;
    public bool consumeItemOnUse = false;

    [Header("Panel Settings")]
    public PanelManager panelManager;
    public TMP_Text feedbackText;

    private InventoryController playerInventory;

    private void Start()
    {
        if (panelManager == null)
        {
            panelManager = GetComponent<PanelManager>();
            if (panelManager == null)
            {
                panelManager = FindObjectOfType<PanelManager>();
            }
        }

        playerInventory = FindObjectOfType<InventoryController>();

        // Hide feedback text if available
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }

    public override void Interact()
    {
        TryUnlock();
    }

    private void TryUnlock()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory not found!");
            return;
        }

        // // Check if player has the required item
        // if (playerInventory.HasKeyForObject(requiredItemId))
        // {
        //     Debug.Log("Unlocked with item: " + requiredItemId);

        //     // Remove the item if needed
        //     if (consumeItemOnUse)
        //     {
        //         // Find the item that unlocks this object
        //         foreach (Item item in playerInventory.inventoryItems)
        //         {
        //             if (item.isKey && item.unlocksObjectId == requiredItemId)
        //             {
        //                 playerInventory.RemoveItem(item.itemName);
        //                 break;
        //             }
        //         }
        //     }

        //     // Show the panel
        //     if (panelManager != null)
        //     {
        //         panelManager.MoveToNextPanel();
        //     }

        //     // Hide feedback text
        //     if (feedbackText != null)
        //     {
        //         feedbackText.gameObject.SetActive(false);
        //     }
        // }
        // else
        // {
        //     // Player doesn't have the required item
        //     Debug.Log("Missing required item: " + requiredItemId);

        //     // Show feedback text if available
        //     if (feedbackText != null)
        //     {
        //         feedbackText.gameObject.SetActive(true);
        //         feedbackText.text = "You need a key to unlock this.";
        //     }
        // }
    }
}
