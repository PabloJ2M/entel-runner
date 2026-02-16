using UnityEngine;

namespace Unity.Tutorial
{
    public class TutorialHandlerObject3D : TutorialHandlerBehaviour
    {
        [SerializeField] private Sprite _overrideShape;

        protected override void Awake() =>
            _step.Element = new Object3DElement(Camera.main, transform, _overrideShape);
    }
}