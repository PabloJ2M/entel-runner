using UnityEngine;
using UnityEngine.UI;

namespace Unity.Tutorial
{
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class TutorialHandlerRect : TutorialHandlerBehaviour
    {
        protected override void Awake() =>
            _step.Element = new RectElement(transform, GetComponent<Image>());
    }
}