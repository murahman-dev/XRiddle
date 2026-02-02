using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHider : MonoBehaviour
{
    //Getting All Canvas References In The Scene
    public Canvas[] uiCanvas;
    public GameObject[] canvasGameObject;

    //Setting The CanvasGroup.alpha = 0 & Deactivating All The Canvas UI's In The Beginning
    void Start()
    {
        for (int i = 1; i < uiCanvas.Length; i++)
        {
            uiCanvas[i].GetComponent<CanvasGroup>().alpha = 0.0f;
            uiCanvas[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < canvasGameObject.Length; i++)
        {
            canvasGameObject[i].SetActive(false);
        }
    }
}