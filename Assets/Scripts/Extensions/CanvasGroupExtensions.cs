using UnityEngine;

namespace CRI.HelloHouston
{
    public static class CanvasGroupExtensions
    {
        public static void Hide(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        public static void Show(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }
}
