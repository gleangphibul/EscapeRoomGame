using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PasswordLockedPanel : Interactable
{
    [Header("Password Settings")]
    public string correctCode = "";
    public int passwordLengthLimit = 0;

    [Header("UI Elements")]
    public GameObject lockedPanel;
    public TMP_InputField inputField;
    public TMP_Text feedbackText;

    [Header("Panel Manager")]
    public PanelManager panelManager;

    private bool isUnlocked = false;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            Debug.Log("Enter key pressed.");
            SubmitCode();
        }

        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     Debug.Log("Resetting panel index.");
        //     ResetPanelIndex();
        // }
    }

    public override void Interact()
    {
        if (isUnlocked)
        {
            panelManager.ShowFinalPanelOnly();
        }
        else
        {
            OpenLockedPanel();
        }
    }

    private void OpenLockedPanel()
    {
        lockedPanel.SetActive(!lockedPanel.activeSelf);
        inputField.text = "";
        feedbackText.text = "";
        feedbackText.gameObject.SetActive(false);

        inputField.ActivateInputField(); 
        inputField.Select();
        
    }

    public void SubmitCode()
    {
        string input = inputField.text.Trim();
        Debug.Log("User submitted: " + input);

        if (input.Length > passwordLengthLimit)
        {
            input = input.Substring(0, passwordLengthLimit);
            inputField.text = input;
            Debug.Log("Trimmed input to length limit: " + input);
        }

        if (input == correctCode)
        {
            Debug.Log("Correct code entered. Unlocking panel.");
            isUnlocked = true;
            panelManager.MoveToNextPanel(); // or ShowFinalPanelOnly() if it's the last step
        }
        else
        {
            Debug.Log("Wrong password entered.");
            feedbackText.gameObject.SetActive(true);
            feedbackText.text = "Wrong password. Try again.";
        }
    }
}
