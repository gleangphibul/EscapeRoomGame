using UnityEngine;
using TMPro;

public class Door : Interactable
{
    [Header("Key Settings")]
    public int requiredKeyID;
    public string keyName;
    
    [Header("Door Visuals")]
    public SpriteRenderer doorRenderer; 
    public Sprite lockedSprite; 
    public Sprite unlockedSprite; 
    
    [Header("Collider")]
    public Collider2D doorCollider; 
    
    [Header("Feedback")]
    public GameObject feedbackTextObject; 
    public TMP_Text feedbackText; 
    public float feedbackDuration = 2f; 
    
    private bool isUnlocked = false;
    private InventoryController inventoryController;
    
    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
        
        if (doorRenderer != null)
        {
            doorRenderer.sprite = lockedSprite;
        }
        
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
        }
        
        if (feedbackTextObject != null)
        {
            feedbackTextObject.SetActive(false);
        }
    }
    
    public override void Interact()
    {
        if (isUnlocked)
        {
            return;
        }
        
        TryUnlockDoor();
    }
    
    private void TryUnlockDoor()
    {
        
        GameObject keyItem = FindKeyInInventory();
        
        if (keyItem != null)
        {
            UnlockDoor();
            RemoveKeyFromInventory(keyItem);
            
            if (feedbackText != null)
            {
                ShowFeedback("Door unlocked!");
            }
        }
        else
        {
           
            if (feedbackText != null)
            {
                ShowFeedback("You need a key to unlock this door.");
            }
        }
    }
    
    private void UnlockDoor()
    {
        isUnlocked = true;
        
        if (doorRenderer != null && unlockedSprite != null)
        {
            doorRenderer.sprite = unlockedSprite;
        }
        
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