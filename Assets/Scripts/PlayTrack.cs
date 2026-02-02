using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrack : MonoBehaviour
{
    [SerializeField] string audioURL;

    public void PlayAudio()
    {
        Application.OpenURL(audioURL);
    }
}
