using UnityEngine;

public class HiddenItem : MonoBehaviour
{
    public GameObject itemPrefab;      // The UI prefab of the item to add to inventory
    public string associatedPanelName; // The name of the panel this item is associated with
    private PanelManager panelManager; // Reference to the panel manager
    private bool isVisible = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D itemCollider;

    void Start()
    {
        // Get references
        panelManager = FindObjectOfType<PanelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<Collider2D>();
        
        // Initially hide the item
        HideItem();
    }

    void Update()
    {
        // Check if we should make the item visible (when we're in the final panel)
        CheckVisibility();
    }

    // Check if this item should be visible based on panel state
    private void CheckVisibility()
    {
        if (panelManager != null)
        {
            // Check if we're at the final panel
            bool isFinalPanel = panelManager.currentPanelIndex == panelManager.openedPanels.Count - 1;
            
            // If we're at the final panel but item is not yet visible, show it
            if (isFinalPanel && !isVisible)
            {
                ShowItem();
            }
            // If we're not at final panel but item is visible, hide it
            else if (!isFinalPanel && isVisible)
            {
                HideItem();
            }
        }
    }

    // Make the item visible and interactable
    private void ShowItem()
    {
        spriteRenderer.enabled = true;
        itemCollider.enabled = true;
        isVisible = true;
        Debug.Log("Item " + gameObject.name + " is now visible");
    }

    // Hide the item and make it non-interactable
    private void HideItem()
    {
        spriteRenderer.enabled = false;
        itemCollider.enabled = false;
        isVisible = false;
    }
}
