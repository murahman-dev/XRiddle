using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Vuforia;

/*
* Manages switching between AR Foundation placement mode and Vuforia image tracking mode.
* The ARSession remains active at all times to maintain the camera feed. 
* Only the ARPlaneManager and VuforiaBehaviour/ObserverBehaviour components are toggled to
* switch between modes. 
* Disabling ARSession directly causes a black screen because both systems 
* share the underlying camera pipeline.
* UI elements use CanvasGroup for visibility toggling to avoid layout recalculation
* overhead from SetActive.
*/
public class SwitchARMode : MonoBehaviour
{
    // Button that switches to placement mode (shown during tracking mode)
    [SerializeField] GameObject placementMode;

    // Button that switches to tracking mode (shown during placement mode)
    [SerializeField] GameObject trackingMode;

    // Crosshair/reticle overlay shown during placement mode
    [SerializeField] CanvasGroup centerImage;

    // The 3D model being placed
    // Uses SetActive as it is not a UI element
    [SerializeField] GameObject modelGameObject;

    [SerializeField] CanvasGroup scaleSlider;
    [SerializeField] CanvasGroup adjustButton;
    [SerializeField] CanvasGroup placeButton;

    // Vuforia's main engine component
    // Disabled to pause image tracking
    [SerializeField] VuforiaBehaviour vuforiaBehaviour;

    // All Vuforia image targets in the scene
    [SerializeField] ObserverBehaviour[] vuforiaImageTargets;

    public ARPlaneManager planeManager;
    public ARPlacementManager placementManager;
    bool inPlacementMode = false;

    void Start()
    {
        // Initialize with all mode-specific UI and subsystems disabled
        // The user selects a mode from the main menu
        CanvasGroupUtility.SetVisible(adjustButton, false);
        CanvasGroupUtility.SetVisible(placeButton, false);
        CanvasGroupUtility.SetVisible(scaleSlider, false);
        CanvasGroupUtility.SetVisible(centerImage, false);
        modelGameObject.SetActive(false);

        // No placement raycasting until placement mode is entered
        placementManager.enabled = false;
        SetVuforiaTracking(false);
    }

    private void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            plane.gameObject.SetActive(inPlacementMode);
        }
    }

    // Activates Vuforia image tracking and disables AR Foundation plane detection
    // Hides all placement-related UI and shows the placement mode button
    public void EnableTrackingMode()
    {
        inPlacementMode = false;
        CanvasGroupUtility.SetVisible(adjustButton, false);
        CanvasGroupUtility.SetVisible(placeButton, false);
        CanvasGroupUtility.SetVisible(scaleSlider, false);
        CanvasGroupUtility.SetVisible(centerImage, false);
        modelGameObject.SetActive(false);
        trackingMode.SetActive(false);
        placementMode.SetActive(true);

        planeManager.enabled = false;
        placementManager.enabled = false;

        // Hide any plane visuals left over from placement mode
        SetAllPlanesActive(false);
        SetVuforiaTracking(true);
    }

    // Activates AR Foundation plane detection and disables Vuforia image tracking
    // Shows all placement-related UI and the tracking mode button
    public void EnablePlacementMode()
    {
        inPlacementMode = true;
        CanvasGroupUtility.SetVisible(adjustButton, false);
        CanvasGroupUtility.SetVisible(placeButton, true);
        CanvasGroupUtility.SetVisible(scaleSlider, true);
        CanvasGroupUtility.SetVisible(centerImage, true);
        modelGameObject.SetActive(true);
        trackingMode.SetActive(true);
        placementMode.SetActive(false);

        SetVuforiaTracking(false);
        planeManager.enabled = true;
        placementManager.enabled = true;
        SetAllPlanesActive(true);
    }

    // Enables or disables the Vuforia engine and every image target together
    private void SetVuforiaTracking(bool active)
    {
        vuforiaBehaviour.enabled = active;
        foreach (ObserverBehaviour target in vuforiaImageTargets)
        {
            target.enabled = active;
        }
    }

    // Uses SetActive since AR planes are 3D tracked objects
    private void SetAllPlanesActive(bool value)
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}