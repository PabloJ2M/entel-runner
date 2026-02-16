using UnityEngine;

namespace Unity.Tutorial
{
    [CreateAssetMenu(fileName = "tutorial_settings", menuName = "tutorial/settings", order = -1)]
    public class SO_TutorialSettings : ScriptableObject
    {
        [Header("Timing")]
        [SerializeField] private float _startDelay = 1f;
        [SerializeField] private float _displayDelay = 0.5f;
        [SerializeField] private float _stepDelay = 0.3f;

        [Header("Comportamiento")]
        [SerializeField] private bool _skipOnInteract = true;
        [SerializeField] private bool _allowPause = true;
        [SerializeField] private bool _autoPlayOnAwake = false;

        [Header("Animación")]
        [SerializeField] private float _transitionDuration = 0.3f;
        [SerializeField] private AnimationCurve _transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public float StartDelay => _startDelay;
        public float DisplayDelay => _displayDelay;
        public float StepDelay => _stepDelay;

        public bool SkipOnInteract => _skipOnInteract;
        public bool AllowPause => _allowPause;
        public bool AutoPlayOnAwake => _autoPlayOnAwake;

        public float TransitionDuration => _transitionDuration;
        public float TransitionCurve(float time) => _transitionCurve.Evaluate(time);
    }
}