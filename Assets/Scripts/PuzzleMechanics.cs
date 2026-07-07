using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
* Word guessing game where the player types a word and submits it for validation.
* Each correct guess advances to the next stage. 
* Upon completing all stages, the UIChanger component is triggered
* to transition to the next screen.
* The required word length and total stage count are derived from the words array.
*/
public class PuzzleMechanics : MonoBehaviour
{
    public TMP_InputField inputField;

    // Individual TMP_Text components that display each letter as the player types
    public TMP_Text[] letterDisplays;

    public TMP_Text feedbackText;
    public TMP_Text stageText;
    public Button submitButton;

    // Array of target words, one per stage
    public string[] words = { "UNITY" };

    private UIChanger uiChanger;

    private int currentStage = 0;

    private void Start()
    {
        submitButton.interactable = true;

        feedbackText.text = "";
        stageText.text = $"{currentStage + 1}/{words.Length}";

        // Retrieve the UIChanger from a child object for screen transitions on completion
        uiChanger = GetComponentInChildren<UIChanger>();

        inputField.onValueChanged.AddListener(OnInputValueChanged);

        ClearInputs();
    }

    private void OnDestroy()
    {
        // Remove the listener to avoid a dangling callback
        if (inputField != null)
        {
            inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        }
    }

    // Updates the individual letter display boxes to mirror the current input
    private void OnInputValueChanged(string value)
    {
        for (int i = 0; i < letterDisplays.Length; i++)
        {
            if (i < value.Length)
                letterDisplays[i].text = value[i].ToString();
            else
                letterDisplays[i].text = "_";
        }
    }

    // Validates the player's guess against the current stage's target word
    // Advances the stage on a correct guess, or triggers the UI transition
    // if all stages are complete
    public void CheckGuess()
    {
        // Guard against post-completion submissions or an empty words array
        if (currentStage >= words.Length)
        {
            return;
        }

        string playerGuess = inputField.text.ToUpper();
        int requiredLength = words[currentStage].Length;

        if (playerGuess.Length != requiredLength)
        {
            feedbackText.enabled = true;
            feedbackText.text = $"Please enter a {requiredLength}-letter word!";
            return;
        }

        if (playerGuess == words[currentStage])
        {
            currentStage++;
            if (currentStage < words.Length)
            {
                feedbackText.enabled = true;
                feedbackText.text = "Correct!";
                stageText.text = $"{currentStage + 1}/{words.Length}";
                ClearInputs();
            }
            else
            {
                // All stages complete
                // Trigger screen transition and disable input
                feedbackText.enabled = true;
                feedbackText.text = "Congratulations!";
                stageText.text = $"{words.Length}/{words.Length}";
                if (uiChanger != null)
                {
                    uiChanger.ChangeUI();
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

    // Resets the input field and letter displays for a new guess attempt
    // Automatically activates the input field to open the on-screen keyboard
    private void ClearInputs()
    {
        inputField.text = "";
        for (int i = 0; i < letterDisplays.Length; i++)
        {
            letterDisplays[i].text = "_";
        }

        if (currentStage < words.Length)
        {
            inputField.characterLimit = words[currentStage].Length;
        }

        inputField.ActivateInputField();
    }
}
