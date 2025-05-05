using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;

    public AudioSource itemCollectedSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
        itemCollectedSound = GetComponent<AudioSource>();
    }

    public void AddItemToInventory(Item item)
    {
       
        item = GetComponent<Item>();
        if(item != null)
        {
            // Add item to inventory
            bool itemAdded = inventoryController.AddItem(gameObject);
            if (itemAdded)
            {
                item.PickUp();
                Destroy(gameObject);
                PlayItemCollectedSound();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if(item != null)
            {
                // Add item to inventory
                bool itemAdded = inventoryController.AddItem(collision.gameObject);

                if (itemAdded)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    private void PlayItemCollectedSound()
    {
        itemCollectedSound.Play();
    }
}

   

