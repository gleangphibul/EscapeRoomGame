using UnityEngine;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    [Header("Panels to show in order")]
    public List<GameObject> openedPanels;

    public int currentPanelIndex = 0;
    private bool isFullyUnlocked = false;

    // Show panel(s) based on state
    public void Interact()
    {
        if (isFullyUnlocked)
        {
            ShowFinalPanelOnly();
        }
        else
        {
            ShowInitialPanel();
        }
    }

    private void ShowInitialPanel()
    {
        if (openedPanels.Count > 0)
        {
            openedPanels[0].SetActive(true);
        }
        else
        {
            Debug.LogWarning("No panels to show.");
        }
    }

    public void MoveToNextPanel()
    {
        if (currentPanelIndex < openedPanels.Count)
        {
            openedPanels[currentPanelIndex].SetActive(false);
        }

        currentPanelIndex++;

        if (currentPanelIndex < openedPanels.Count)
        {
            openedPanels[currentPanelIndex].SetActive(true);
        }

        // If we've reached the last one
        if (currentPanelIndex >= openedPanels.Count - 1)
        {
            isFullyUnlocked = true;
        }
    }

    public void ShowFinalPanelOnly()
    {
        for (int i = 0; i < openedPanels.Count; i++)
        {
            if (i == openedPanels.Count - 1)
            {
                openedPanels[i].SetActive(true);
            }
            else
            {
                openedPanels[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        // Reset index to be the last one to not be out of bounds
        if (currentPanelIndex > openedPanels.Count - 1)
        {
            currentPanelIndex = openedPanels.Count - 1;
        }
        // Close the last panel when the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && (currentPanelIndex == 0 || currentPanelIndex == openedPanels.Count - 1))
        {
            CloseLastPanel();
        }
    }

    private void CloseLastPanel()
    {
        if (openedPanels.Count > 0)
        {
            openedPanels[currentPanelIndex].SetActive(false);
        } 
        else
        {
            Debug.LogWarning("No panels to close.");
        }
    }
}

