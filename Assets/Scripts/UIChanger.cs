using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChanger : MonoBehaviour
{
    //References To The Two UI's That We Will Switch Between
    [SerializeField] Canvas firstCanvas;
    [SerializeField] Canvas secondCanvas;

    //References To The Two CanvasGroups Of The Two UI's That We Are Dealing With For Manipulating Alpha Value
    [SerializeField] CanvasGroup firstCanvasGroup;
    [SerializeField] CanvasGroup secondCanvasGroup;

    //Fade Amount
    float fadeAmount = 0f;

    //Initializing The Values Of CanvasGroup & Buttons In Runtime For Both UI's
    private void Start()
    {
        
    }

    //Change UI Method For Triggering The First Co-Routine
    public void ChangeUI()
    {
        StartCoroutine(FadeIn());
    }

    //Fade In Of The First UI
    IEnumerator FadeIn()
    {
        if (firstCanvasGroup != null)
        {
            for (fadeAmount = 1; fadeAmount >= 0; fadeAmount -= Time.deltaTime)
            {
                firstCanvasGroup.alpha = fadeAmount;
                yield return null;
            }

            //Starting The Second UI Co-Routine After The First One Is Faded
            StartCoroutine(FadeOut());
        }
    }

    //Fade Out Of The Second UI
    IEnumerator FadeOut()
    {
        if (secondCanvasGroup != null)
        {
            secondCanvas.gameObject.SetActive(true);

            for (fadeAmount = 0; fadeAmount <= 1; fadeAmount += Time.deltaTime)
            {
                secondCanvasGroup.alpha = fadeAmount;
                yield return null;
            }

            //Making The CanvasGroup.alpha = 1 If The Values Are Too Far Away From Being 1
            if (!Mathf.Approximately((firstCanvasGroup.alpha), 1.0f))
            {
                firstCanvasGroup.alpha = 1.0f;
            }
            else if (!Mathf.Approximately((secondCanvasGroup.alpha), 1.0f))
            {
                secondCanvasGroup.alpha = 1.0f;
            }

            //When Second UI Appears, We Deactivate The First One Otherwise It Messes Up The References For The Second UI
            firstCanvas.gameObject.SetActive(false);
        }
    }
}
