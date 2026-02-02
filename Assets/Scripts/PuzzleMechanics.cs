using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WordGuessGame : MonoBehaviour
{
    public TMP_InputField inputField; // One TMP_InputField for the whole word
    public TMP_Text[] letterDisplays; // Array of TMP_Text components to display letters
    public TMP_Text feedbackText; // TMP_Text for feedback
    public TMP_Text stageText; // TMP_Text for the stage number
    public Button submitButton; // Button for submitting the guess
    public string[] words = { "FLOYD" }; // Array of target words
    private UIChanger uiChanger;

    private int currentStage = 0; // Tracks the current stage

    private void Start()
    {
        if (submitButton.interactable == false)
        {
            submitButton.interactable = true;
        }

        feedbackText.text = "HELLO";
        stageText.text = $"{currentStage + 1}/5";
        uiChanger = GetComponentInChildren<UIChanger>();

        // Add listener to handle input changes
        inputField.onValueChanged.AddListener(OnInputValueChanged);

        // Clear inputs and set initial focus
        ClearInputs();
    }

    // Handle changes in the input field (when the player types)
    private void OnInputValueChanged(string value)
    {
        // Update the letter displays as the player types
        for (int i = 0; i < letterDisplays.Length; i++)
        {
            if (i < value.Length)
                letterDisplays[i].text = value[i].ToString(); // Update text
            else
                letterDisplays[i].text = "_"; // Display underscores if there are fewer than 5 characters
        }
    }

    public void CheckGuess()
    {
        string playerGuess = inputField.text.ToUpper();

        // Make sure the input is exactly 5 letters
        if (playerGuess.Length != 5)
        {
            feedbackText.enabled = true;
            feedbackText.text = "Please enter a 5-letter word!";
            return;
        }

        // Check if the guess is correct
        if (playerGuess == words[currentStage])
        {
            currentStage++;
            if (currentStage < words.Length)
            {
                feedbackText.enabled = true;
                feedbackText.text = "Correct!";
                stageText.text = $"{currentStage + 1}/5";
                ClearInputs();
            }
            else
            {
                feedbackText.enabled = true;
                feedbackText.text = "Congratulations!";
                if (uiChanger != null)
                {
                    uiChanger.ChangeUI(); // Change UI if game is finished
                }
                submitButton.interactable = false;
            }
        }
        else
        {
            feedbackText.enabled = true;
            feedbackText.text = "Incorrect! Try Again";
        }
    }

    // Clear the input field and reset the letter displays
    private void ClearInputs()
    {
        inputField.text = "";
        for (int i = 0; i < letterDisplays.Length; i++)
        {
            letterDisplays[i].text = "_"; // Reset the letter display
        }

        inputField.ActivateInputField(); // Activate the input field to open the keyboard once
    }
}
