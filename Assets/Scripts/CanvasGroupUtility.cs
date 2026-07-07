using UnityEngine;

/*
* Shared helper for toggling CanvasGroup visibility, interactivity, and raycast blocking.
*/
public static class CanvasGroupUtility
{
    // Sets a CanvasGroup's alpha, interactivity, and raycast blocking in one call
    public static void SetVisible(CanvasGroup group, bool active)
    {
        if (group == null)
        {
            Debug.LogWarning("CanvasGroupUtility: CanvasGroup reference is not assigned.");
            return;
        }

        group.alpha = active ? 1f : 0f;
        group.interactable = active;
        group.blocksRaycasts = active;
    }
}
