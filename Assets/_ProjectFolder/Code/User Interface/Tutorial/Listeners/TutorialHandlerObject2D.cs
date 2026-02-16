using UnityEngine;

namespace Unity.Tutorial
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TutorialHandlerObject2D : TutorialHandlerBehaviour
    {
        protected override void Awake() =>
            _step.Element = new Object2DElement(Camera.main, transform, GetComponent<SpriteRenderer>());
    }
}