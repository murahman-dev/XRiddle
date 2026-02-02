using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoText;
    [TextArea]
    public string infoString;

    public void ShowInfoText()
    {
        infoText.text = infoString;
    }
}
