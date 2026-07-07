using UnityEngine;

/*
* Provides application quit functionality for a UI button.
*/
public class QuitGame : MonoBehaviour
{
    public void QuitGameFunctionality()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
