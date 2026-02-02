using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ScaleController : MonoBehaviour
{
    XROrigin m_XROrigin;
    public Slider scaleSlider;

    private void Awake()
    {
        m_XROrigin = GetComponent<XROrigin>();
    }
    void Start()
    {
        scaleSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    public void OnSliderValueChanged(float value)
    {
        m_XROrigin.transform.localScale = Vector3.one / value;
    }
}
