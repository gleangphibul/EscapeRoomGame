using UnityEngine;

public class Board : Interactable
{
    public GameObject boardPanel;

    public override void Interact()
    {
        if (boardPanel != null)
        {
            boardPanel.SetActive(!boardPanel.activeSelf);
        }
    }
}

