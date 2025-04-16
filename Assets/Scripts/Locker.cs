using UnityEngine;
using TMPro;

public class Locker : Interactable
{
//     public GameObject lockerPanel;
//     public TMP_InputField codeInputField;
//     // public TMP_Text feedbackText;
//     public string correctCode = "ABCDEFG"; // example

//     private bool panelActive = false;

    public override void Interact()
    {
//         if (panelActive) {
//             ClosePanel();
//         } else {
//             OpenPanel();
//         }
    }

//     void Update()
//     {
//         if (!panelActive) return;

//         // Submit code with Enter
//         if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && codeInputField.isFocused)
//         {
//             SubmitCode();
//         }

//         // Close panel with Space
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             ClosePanel();
//         }
//     }

//     private void OpenPanel()
//     {
//         lockerPanel.SetActive(true);
//         // feedbackText.text = "";
//         codeInputField.text = "";
//         codeInputField.ActivateInputField();
//         panelActive = true;
//     }

//     private void ClosePanel()
//     {
//         lockerPanel.SetActive(false);
//         panelActive = false;
//     }

//     public void SubmitCode()
//     {
//         string input = codeInputField.text.ToUpper();

//         if (input == correctCode)
//         {
//             // feedbackText.text = "Correct!";
//             Debug.Log("Locker opened!");
//             ClosePanel();
//         }
//         else
//         {
//             // feedbackText.text = "Try again.";
//         }
//     }
}
