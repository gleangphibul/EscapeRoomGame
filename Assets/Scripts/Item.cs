using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID; 
    public string Name;

    public virtual void UseItem()
    {
        Debug.Log("Using item" + Name);
    }
    public virtual void PickUp()
    {
    }


}