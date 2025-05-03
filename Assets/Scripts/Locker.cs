using UnityEngine;
using TMPro;

public class Locker : Interactable
{
    public GameObject lockerPanel;
    public GameObject lockerOpenedPanel;           // The panel shown when password is correct
    public TMP_InputField codeInputField;
    public TMP_Text lockedText;
    public TMP_Text feedbackText;
    public string correctCode = "SAPHONY";

    private bool panelActive = false;

    public override void Interact()
    {
        if (lockerPanel != null || lockerOpenedPanel != null)
        {
            lockerPanel.SetActive(!lockerPanel.activeSelf);
        }
    }

    void Update()
    {
        if (!panelActive) return;

        // Close panel with Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }

        // Submit code with Enter, only if input field is focused
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && codeInputField.isFocused)
        {
            SubmitCode();
        }
    }

    private void OpenPanel()
    {
        lockerPanel.SetActive(true);
        codeInputField.text = "";
        feedbackText.text = "";
        panelActive = true;
    }

    private void ClosePanel()
    {
        lockerPanel.SetActive(false);
        panelActive = false;
    }

    public void SubmitCode()
    {
        string input = codeInputField.text.ToUpper();

        // Limit input to 7 characters
        if (input.Length > 7)
        {
            input = input.Substring(0, 7);
            codeInputField.text = input;
        }

        if (input == correctCode)
        {
            Debug.Log("Correct code entered!");
            ClosePanel();

            if (lockerOpenedPanel != null)
                lockerOpenedPanel.SetActive(true);
        }
        else
        {
            feedbackText.gameObject.SetActive(true); // Show the feedback
            Debug.Log("Wrong password entered.");
        }
    }
}

