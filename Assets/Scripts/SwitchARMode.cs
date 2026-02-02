using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Vuforia;

public class SwitchARMode : MonoBehaviour
{
    [SerializeField] GameObject placementMode;
    [SerializeField] GameObject trackingMode;
    [SerializeField] GameObject centerImage;
    [SerializeField] GameObject labyrinthGameObject;
    [SerializeField] GameObject scaleSlider;
    [SerializeField] GameObject adjustButton;
    [SerializeField] GameObject placeButton;
    [SerializeField] GameObject xrOriginCamera;
    [SerializeField] GameObject[] vuforiaImageTargets;

    public ARSession arSession;
    public ARPlaneManager planeManager;
    void Start()
    {
        adjustButton.SetActive(false);
        placeButton.SetActive(false);
        scaleSlider.SetActive(false);
        centerImage.SetActive(false);
        labyrinthGameObject.SetActive(false);
        foreach (GameObject imageTarget in vuforiaImageTargets)
        {
            imageTarget.SetActive(false);
        }
    }

    public void EnableTrackingMode()
    {
        adjustButton.SetActive(false);
        placeButton.SetActive(false);
        labyrinthGameObject.SetActive(false);
        scaleSlider.SetActive(false);
        centerImage.SetActive(false);
        trackingMode.SetActive(false);
        placementMode.SetActive(true);
        foreach (GameObject imageTarget in vuforiaImageTargets)
        {
            imageTarget.SetActive(true);
        }
    }

    public void EnablePlacementMode()
    {
        adjustButton.SetActive(false);
        placeButton.SetActive(true);
        labyrinthGameObject.SetActive(true);
        scaleSlider.SetActive(true);
        centerImage.SetActive(true);
        trackingMode.SetActive(true);
        placementMode.SetActive(false);
        foreach (GameObject imageTarget in vuforiaImageTargets)
        {
            imageTarget.SetActive(false);
        }
    }
}
