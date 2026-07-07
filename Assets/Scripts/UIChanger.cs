using System.Collections;
using UnityEngine;

/*
* Crossfades between two UI screens using CanvasGroup alpha transitions.
* The first canvas fades out, then the second canvas fades in.
*/
public class UIChanger : MonoBehaviour
{
    // The CanvasGroup of the screen being faded out
    [SerializeField] CanvasGroup firstCanvasGroup;

    // The CanvasGroup of the screen being faded in
    [SerializeField] CanvasGroup secondCanvasGroup;

    // Prevents overlapping transitions if ChangeUI is triggered again mid-fade
    private bool isTransitioning = false;

    // Initiates the crossfade transition from the first canvas to the second
    // Ignored while a transition is already running
    public void ChangeUI()
    {
        if (isTransitioning)
        {
            return;
        }

        StartCoroutine(CrossFade());
    }

    // Sequentially fades out the first canvas, then fades in the second
    // Each fade runs over approximately one second using Time.deltaTime interpolation
    IEnumerator CrossFade()
    {
        if (firstCanvasGroup == null || secondCanvasGroup == null) yield break;

        isTransitioning = true;

        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            firstCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }
        CanvasGroupUtility.SetVisible(firstCanvasGroup, false);

        // Ensure second canvas is visible before fading in
        secondCanvasGroup.alpha = 0f;
        secondCanvasGroup.gameObject.SetActive(true);

        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            secondCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }
        CanvasGroupUtility.SetVisible(secondCanvasGroup, true);

        isTransitioning = false;
    }
}
