using UnityEngine;

public class Openable : Interactable
{
    private bool isOpen;
    public override void Interact()
    {
        if (isOpen) {
            Debug.Log("closed");
        } else {
            Debug.Log("opened");
        }
        isOpen = !isOpen;
    }
}
