using TMPro;
using UnityEngine;

/*
* Displays context-specific information in a shared text panel.
* Each instance is attached to a different UI button with its own
* infoString configured in the Inspector. 
* All instances write to the same shared TextMeshProUGUI component.
*/
public class ShowInfo : MonoBehaviour
{
    // Shared text component that all ShowInfo instances write to
    [SerializeField] TextMeshProUGUI infoText;

    // The text content this particular button displays when pressed
    [TextArea]
    public string infoString;

    public void ShowInfoText()
    {
        infoText.text = infoString;
    }
}
