using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject canvasGameObject;
    [SerializeField] ImageTargetBehaviour imageTargetBehaviour;
    [SerializeField] Transform detachImageTarget;
    private void Start()
    {
        imageTargetBehaviour = GetComponent<ImageTargetBehaviour>();
        if (imageTargetBehaviour != null)
        {
            imageTargetBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            ShowCanvas();
        }
    }
    public void ShowCanvas()
    {
        if (canvasGameObject != null)
        {
            if (detachImageTarget != null)
            {
                if (canvasGameObject.activeInHierarchy == false)
                {
                    canvasGameObject.SetActive(true);
                    canvasGameObject.GetComponent<Canvas>().enabled = true;
                    Debug.Log("Show UI");
                }
                canvasGameObject.transform.SetParent(detachImageTarget.transform, true);
            }
        }
    }
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
    }
}
