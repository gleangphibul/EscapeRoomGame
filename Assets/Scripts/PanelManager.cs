// using UnityEngine;
// using System.Collections.Generic;

// public class PanelManager : MonoBehaviour
// {
//     [Header("Panels to show in order")]
//     public List<GameObject> openedPanels;

//     private int currentPanelIndex = 0;
//     private bool isFullyUnlocked = false;

//     // Show panel(s) based on state
//     public void Interact()
//     {
//         if (isFullyUnlocked)
//         {
//             ShowFinalPanelOnly();
//         }
//         else
//         {
//             ShowInitialPanel();
//         }
//     }

//     private void ShowInitialPanel()
//     {
//         if (openedPanels.Count > 0)
//         {
//             openedPanels[0].SetActive(true);
//         }
//     }

//     public void MoveToNextPanel()
//     {
//         if (currentPanelIndex < openedPanels.Count)
//         {
//             openedPanels[currentPanelIndex].SetActive(false);
//         }

//         currentPanelIndex++;

//         if (currentPanelIndex < openedPanels.Count)
//         {
//             openedPanels[currentPanelIndex].SetActive(true);
//         }

//         // If we've reached the last one
//         if (currentPanelIndex >= openedPanels.Count - 1)
//         {
//             isFullyUnlocked = true;
//         }
//     }

//     public void ShowFinalPanelOnly()
//     {
//         for (int i = 0; i < openedPanels.Count; i++)
//         {
//             openedPanels[i].SetActive(i == openedPanels.Count - 1);
//         }
//     }

//     public void ResetPanels()
//     {
//         foreach (var panel in openedPanels)
//         {
//             panel.SetActive(false);
//         }
//         currentPanelIndex = 0;
//         isFullyUnlocked = false;
//     }

//     void Update()
//     {
//         // Close the last panel when the escape key is pressed
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             CloseLastPanel();
//         }
//     }

//     private void CloseLastPanel()
//     {
//         openedPanels[currentPanelIndex].SetActive(false);
//     }
// }
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
        Debug.Log("Interact called. isFullyUnlocked: " + isFullyUnlocked);
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
            Debug.Log("Showing initial panel: " + openedPanels[0].name);
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
            Debug.Log("Hiding panel: " + openedPanels[currentPanelIndex].name);
            openedPanels[currentPanelIndex].SetActive(false);
        }

        currentPanelIndex++;
        Debug.Log("Moving to panel index: " + currentPanelIndex);

        if (currentPanelIndex < openedPanels.Count)
        {
            Debug.Log("Showing panel: " + openedPanels[currentPanelIndex].name);
            openedPanels[currentPanelIndex].SetActive(true);
        }

        // If we've reached the last one
        if (currentPanelIndex >= openedPanels.Count - 1)
        {
            isFullyUnlocked = true;
            Debug.Log("Last panel unlocked.");
        }
    }

    public void ShowFinalPanelOnly()
    {
        Debug.Log("Showing final panel only.");
        for (int i = 0; i < openedPanels.Count; i++)
        {
            if (i == openedPanels.Count - 1)
            {
                Debug.Log("Showing last panel: " + openedPanels[i].name);
                openedPanels[i].SetActive(true);
            }
            else
            {
                Debug.Log("Hiding panel: " + openedPanels[i].name);
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
            Debug.Log("Escape key pressed.");
            CloseLastPanel();
        }
    }

    private void CloseLastPanel()
    {
        if (openedPanels.Count > 0)
        {
            Debug.Log("Closing panel: " + openedPanels[currentPanelIndex].name);
            openedPanels[currentPanelIndex].SetActive(false);
        } 
        else
        {
            Debug.LogWarning("No panels to close.");
        }
    }
}

