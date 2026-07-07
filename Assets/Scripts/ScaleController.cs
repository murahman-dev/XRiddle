using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

/*
* Controls the scale of the AR scene using a UI slider.
* Applies an inverse scale to the XR Origin.
* Increasing the slider value shrinks the world,
* making the placed model appear larger.
*/
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

    private void OnDestroy()
    {
        // Remove the listener to avoid a dangling callback
        if (scaleSlider != null)
        {
            scaleSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }

    // Applies inverse scaling to the XR Origin based on the slider value
    // A value of 2 halves the world scale, making the model appear twice as large
    public void OnSliderValueChanged(float value)
    {
        // Guard against zero or negative slider values
        if (value <= 0f)
        {
            return;
        }

        m_XROrigin.transform.localScale = Vector3.one / value;
    }
}
