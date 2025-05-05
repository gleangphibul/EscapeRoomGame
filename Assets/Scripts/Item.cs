using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID; 
    public string itemName;
    public string unlocksObjectId; // The ID of the object this item can unlock
    public bool isKey = true;      // Since all items are keys

    public virtual void UseItem()
    {
        Debug.Log("Using item: " + itemName);
    }

    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        // You can extend this to display the icon in UI
    }
}
