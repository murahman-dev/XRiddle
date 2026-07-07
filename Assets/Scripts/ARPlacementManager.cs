using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/*
* Positions the 3d model onto detected AR planes by raycasting
* from the center of the screen. 
* The model continuously follows the nearest detected surface until placement is locked.
* Enabled only while placement mode is active. 
* The raycast runs on alternate frames and skips out entirely
* while no planes have been detected yet.
*/
public class ARPlacementManager : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;
    ARPlaneManager m_ARPlaneManager;
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    [SerializeField] Camera arCamera;
    [SerializeField] GameObject model3D;

    private void Awake()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        // Every other frame is enough for smooth surface following
        if (Time.frameCount % 2 != 0)
        {
            return;
        }

        // Nothing to hit until at least one plane has been detected
        if (m_ARPlaneManager == null || m_ARPlaneManager.trackables.count == 0)
        {
            return;
        }

        // Cast a ray from the center of the screen onto AR-detected planes
        Vector3 centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = arCamera.ScreenPointToRay(centerOfScreen);

        // Move the 3d model to the hit position if a valid plane is detected
        if (m_ARRaycastManager.Raycast(ray, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycastHits[0].pose;
            Vector3 positionToBePlaced = hitPose.position;
            model3D.transform.position = positionToBePlaced;
        }
    }
}