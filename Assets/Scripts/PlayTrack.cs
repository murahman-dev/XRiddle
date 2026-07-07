using UnityEngine;

/*
* Opens an external audio URL when triggered by a UI button.
* The URL is configured per-instance in the Inspector.
*/
public class PlayTrack : MonoBehaviour
{
    [SerializeField] string audioURL;

    // Opens the configured audio URL in the device's default browser or app
    // Logs a warning instead of opening a blank page if no URL is assigned
    public void PlayAudio()
    {
        if (string.IsNullOrEmpty(audioURL))
        {
            Debug.LogWarning($"PlayTrack on {gameObject.name}: audioURL is not assigned.");
            return;
        }

        Application.OpenURL(audioURL);
    }
}
