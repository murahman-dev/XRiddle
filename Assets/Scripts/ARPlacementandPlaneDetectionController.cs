using UnityEngine;
using UnityEngine.XR.ARFoundation;

/*
* Toggles AR plane detection and model placement on or off.
* When enabled, the model follows detected surfaces and can be repositioned.
* When disabled, the model is locked in place and planes are hidden.
* UI buttons use CanvasGroup for visibility toggling. 
* AR plane GameObjects use SetActive since they are 3D tracked objects,
* not UI elements.
*/
public class ARPlacementandPlaneDetectionController : MonoBehaviour
{
    ARPlaneManager m_ARPlaneManager;
    ARPlacementManager m_ARPlacementManager;
    [SerializeField] CanvasGroup adjustButton;
    [SerializeField] CanvasGroup placeButton;
    [SerializeField] CanvasGroup scaleSlider;

    private void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        m_ARPlacementManager = GetComponent<ARPlacementManager>();
    }

    // Enables plane detection and model placement
    // Shows the place button and scale slider
    // Hides the adjust button, and makes all detected planes visible
    public void EnableARPlacementandPlaneDetection()
    {
        m_ARPlaneManager.enabled = true;
        m_ARPlacementManager.enabled = true;

        CanvasGroupUtility.SetVisible(placeButton, true);
        CanvasGroupUtility.SetVisible(scaleSlider, true);
        CanvasGroupUtility.SetVisible(adjustButton, false);
        SetAllPlanesActive(true);
    }

    // Disables plane detection and locks the model in its current position
    // Shows the adjust button, hides the place button, scale slider,
    // and all detected planes
    public void DisableARPlacementandPlaneDetection()
    {
        m_ARPlaneManager.enabled = false;
        m_ARPlacementManager.enabled = false;

        CanvasGroupUtility.SetVisible(adjustButton, true);
        CanvasGroupUtility.SetVisible(placeButton, false);
        CanvasGroupUtility.SetVisible(scaleSlider, false);
        SetAllPlanesActive(false);
    }

    // Uses SetActive since AR planes are 3D tracked objects
    private void SetAllPlanesActive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}
