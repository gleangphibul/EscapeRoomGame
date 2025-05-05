using UnityEngine;
using TMPro;

public class Door : Interactable
{
    [Header("Key Settings")]
    public int requiredKeyID; // The ID of the key item needed to unlock this door
    public string keyName; // Optional: The name of the key for debugging
    
    [Header("Door Visuals")]
    public SpriteRenderer doorRenderer; // The sprite renderer of the door
    public Sprite lockedSprite; // Sprite when door is locked
    public Sprite unlockedSprite; // Sprite when door is unlocked
    
    [Header("Collider")]
    public Collider2D doorCollider; // Collider that blocks the player from passing
    
    [Header("Feedback")]
    public GameObject feedbackTextObject; // Optional: Text object to show messages
    public TMP_Text feedbackText; // Optional: Text component for feedback
    public float feedbackDuration = 2f; // How long to show feedback messages
    
    private bool isUnlocked = false;
    private InventoryController inventoryController;
    
    void Start()
    {
        // Get reference to inventory controller
        inventoryController = FindObjectOfType<InventoryController>();
        
        // Set initial door state
        if (doorRenderer != null)
        {
            doorRenderer.sprite = lockedSprite;
        }
        
        // Make sure collider is active
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
        }
        
        // Hide feedback text initially
        if (feedbackTextObject != null)
        {
            feedbackTextObject.SetActive(false);
        }
    }
    
    public override void Interact()
    {
        if (isUnlocked)
        {
            Debug.Log("Door is already unlocked.");
            return;
        }
        
        TryUnlockDoor();
    }
    
    private void TryUnlockDoor()
    {
        Debug.Log("Checking for key with ID: " + requiredKeyID);
        
        // Find the key in inventory
        GameObject keyItem = FindKeyInInventory();
        
        if (keyItem != null)
        {
            Debug.Log("Found key: " + keyName + ". Unlocking door.");
            
            // Unlock door
            UnlockDoor();
            
            // Remove key from inventory
            RemoveKeyFromInventory(keyItem);
            
            // Show success message if feedback exists
            if (feedbackText != null)
            {
                ShowFeedback("Door unlocked!");
            }
        }
        else
        {
            Debug.Log("Key not found in inventory");
            
            // Show error message if feedback exists
            if (feedbackText != null)
            {
                ShowFeedback("You need a key to unlock this door.");
            }
        }
    }
    
    private void UnlockDoor()
    {
        isUnlocked = true;
        
        // Change door sprite to unlocked version
        if (doorRenderer != null && unlockedSprite != null)
        {
            doorRenderer.sprite = unlockedSprite;
        }
        
        // Disable the collider so player can pass through
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
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
    
    private void ShowFeedback(string message)
    {
        if (feedbackTextObject != null && feedbackText != null)
        {
            feedbackText.text = message;
            feedbackTextObject.SetActive(true);
            
            // Auto-hide feedback after a delay
            CancelInvoke("HideFeedback");
            Invoke("HideFeedback", feedbackDuration);
        }
    }
    
    private void HideFeedback()
    {
        if (feedbackTextObject != null)
        {
            feedbackTextObject.SetActive(false);
        }
    }
}