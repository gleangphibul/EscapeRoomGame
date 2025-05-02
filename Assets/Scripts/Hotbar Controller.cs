using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{

    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 5; // 1-0 on the keyboard 
    private Key[] hotbarKeys;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void Awake()
    {
        // Hotbar keys based on slot count
        hotbarKeys = new Key[slotCount];
        for(int i = 0; i < slotCount; i++)
        {
            hotbarKeys[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
        }

        // Create slots
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, hotbarPanel.transform);
            slot.name = $"Slot_{i}";
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Check for key presses
        for(int i = 0; i < slotCount; i++)
        {
            if(Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                // Use item
                UseItemInSlot(i);
            }
        }
    }

    void UseItemInSlot(int index)
    {
        Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        if(slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
            item.UseItem();
        }
    }
}
