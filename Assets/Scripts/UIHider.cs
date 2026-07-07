using UnityEngine;

/*
* Hides all UI screens except the first one on application startup.
* The first canvas in the uiCanvas array (index 0) remains visible as the initial screen.
* All subsequent canvases have their CanvasGroup alpha set to 0 and are deactivated.
* Uses SetActive rather than CanvasGroup-only toggling because some of the managed
* canvases interact with Vuforia's DefaultObserverEventHandler. 
* The handler toggles Canvas components on tracking changes, so component-level
* hiding would be overwritten the first time a target is found.
*/
public class UIHider : MonoBehaviour
{
    // All UI canvases in the application
    // Index 0 stays visible, the rest are hidden
    public Canvas[] uiCanvas;

    // Additional GameObjects that should be hidden at startup
    public GameObject[] canvasGameObject;

    void Start()
    {
        // Hide all canvases except the first one (the initial screen)
        for (int i = 1; i < uiCanvas.Length; i++)
        {
            if (uiCanvas[i].TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup.alpha = 0.0f;
            }
            uiCanvas[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < canvasGameObject.Length; i++)
        {
            canvasGameObject[i].SetActive(false);
        }
    }
}
