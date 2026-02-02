using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementandPlaneDetectionController : MonoBehaviour
{
    ARPlaneManager m_ARPlaneManager;
    ARPlacementManager m_ARPlacementManager;
    [SerializeField] GameObject adjustButton;
    [SerializeField] GameObject placeButton;
    [SerializeField] GameObject scaleSlider;

    private void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        m_ARPlacementManager = GetComponent<ARPlacementManager>();
    }
    public void EnableARPlacementandPlaneDetection()
    {
        m_ARPlaneManager.enabled = true;
        m_ARPlacementManager.enabled = true;

        placeButton.SetActive(true);
        scaleSlider.SetActive(true);
        adjustButton.SetActive(false);
        SetAllPlanesActiveOrDeactive(true);
    }
    public void DisableARPlacementandPlaneDetection()
    {
        m_ARPlaneManager.enabled = false;
        m_ARPlacementManager.enabled = false;

        adjustButton.SetActive(true);
        placeButton.SetActive(false);
        scaleSlider.SetActive(false);
        SetAllPlanesActiveOrDeactive(false);
    }
    private void SetAllPlanesActiveOrDeactive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}
