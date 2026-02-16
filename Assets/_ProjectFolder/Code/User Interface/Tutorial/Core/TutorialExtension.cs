using UnityEngine;

namespace Unity.Tutorial
{
    public static class TutorialExtension
    {
        public static void SetPosition(this RectTransform transform, IElement element)
        {
            transform.sizeDelta = element.Rect.size;
            transform.position = element.Position;
            transform.pivot = element.Pivot;
        }
    }
}