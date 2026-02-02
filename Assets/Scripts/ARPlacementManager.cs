using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{

    ARRaycastManager m_ARRaycastManager;
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    public Camera arCamera;
    public GameObject labyrinthGameObject;

    private void Awake()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
    }
    void Update()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = arCamera.ScreenPointToRay(centerOfScreen);

        if (m_ARRaycastManager.Raycast(ray, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycastHits[0].pose;
            Vector3 positionToBePlaced = hitPose.position;
            labyrinthGameObject.transform.position = positionToBePlaced;
        }
    }
}
