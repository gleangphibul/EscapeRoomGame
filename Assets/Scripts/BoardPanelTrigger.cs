using UnityEngine;

public class BoardPanelTrigger : Interactable
{
    public GameObject hintPanel;

    public override void Interact()
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(!hintPanel.activeSelf);
        }
    }
}

