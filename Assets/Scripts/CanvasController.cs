using UnityEngine;
using Vuforia;

/*
* Controls the visibility of a UI canvas associated with a Vuforia image target.
* When the target is tracked, the canvas is activated and reparented to a persistent
* transform so that Vuforia's DefaultObserverEventHandler cannot disable it.
* Uses SetActive instead of CanvasGroup because Vuforia's DefaultObserverEventHandler
* disables Renderer, Collider, and Canvas components when a target is lost.
* Only one canvas is visible at a time. 
* Tracking a new target hides the previous canvas,
* so different image targets swap content instead of stacking.
*/
public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject canvasGameObject;
    [SerializeField] ImageTargetBehaviour imageTargetBehaviour;

    // Persistent transform to reparent the canvas to after tracking
    [SerializeField] Transform detachImageTarget;

    // The controller whose canvas is currently on screen
    static CanvasController ActiveController;

    private void Start()
    {
        // Falls back to the component on this GameObject if no reference is assigned
        if (imageTargetBehaviour == null)
        {
            imageTargetBehaviour = GetComponent<ImageTargetBehaviour>();
        }
        if (imageTargetBehaviour != null)
        {
            imageTargetBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid a dangling delegate if this controller
        // is destroyed before the image target
        if (imageTargetBehaviour != null)
        {
            imageTargetBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    // Called by Vuforia whenever the image target's tracking status changes
    // Shows the canvas when the target is actively tracked or extended-tracked
    private void OnTargetStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            ShowCanvas();
        }
    }

    // Activates the canvas and reparents it to the detach transform so it persists
    // independently of the image target's active state
    public void ShowCanvas()
    {
        if (canvasGameObject == null)
        {
            return;
        }

        // Latest tracked wins, hide whichever canvas is currently showing
        if (ActiveController != null && ActiveController != this)
        {
            ActiveController.HideCanvas();
        }
        ActiveController = this;

        // Only activate if currently inactive to avoid redundant calls
        if (canvasGameObject.activeInHierarchy == false)
        {
            canvasGameObject.SetActive(true);
            if (canvasGameObject.TryGetComponent(out Canvas canvas))
            {
                canvas.enabled = true;
            }
            Debug.Log("Show UI");
        }

        // Reparent to the persistent transform, out of the handler's reach
        if (detachImageTarget != null)
        {
            canvasGameObject.transform.SetParent(detachImageTarget.transform, true);
        }
    }

    // Deactivates the canvas and reparents it back under the image target
    // so it is ready for the next tracking event
    public void HideCanvas()
    {
        if (canvasGameObject != null)
        {
            if (canvasGameObject.activeInHierarchy == true)
            {
                canvasGameObject.SetActive(false);
                Debug.Log("Hide UI");
            }
            if (detachImageTarget != null)
            {
                canvasGameObject.transform.SetParent(transform, true);
            }
        }

        if (ActiveController == this)
        {
            ActiveController = null;
        }
    }
}