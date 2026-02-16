using System;
using System.Collections;
using UnityEngine;

namespace Unity.Tutorial
{
    public class TutorialManager : SingletonBasic<TutorialManager>
    {
        [SerializeField] private SO_StepList _stepList;
        [SerializeField] private float _startDelay;

        public event Action OnTutorialStarted, OnTutorialCompleted;
        public event Action<SO_Step> OnStepStarted;
        public event Action OnStepCompleted;

        private bool _waitForDisplay, _waitToContinue;
        private SO_Step _currentStep;

        private IEnumerator Start()
        {
            OnTutorialStarted?.Invoke();
            yield return WaitForUnscaledSeconds(_startDelay);

            WaitWhile waitForDisplay = new(() => _waitForDisplay);
            WaitWhile waitToContinue = new(() => _waitToContinue);

            for (int i = 0; i < _stepList.Length; i++)
            {
                _currentStep = _stepList[i];

                _waitToContinue = _currentStep.Type.HasFlag(TutorialType.WaitForInteraction);
                _waitForDisplay = true;

                OnStepStarted?.Invoke(_currentStep);
                yield return waitForDisplay;

                OnStepCompleted?.Invoke();
                _currentStep.onInteracted = InteractHandler;
                yield return waitToContinue;

                _currentStep.onInteracted = null;
            }

            OnTutorialCompleted?.Invoke();
        }
        private IEnumerator WaitForUnscaledSeconds(float seconds)
        {
            float elapsed = 0f;

            while (elapsed < seconds)
            {
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        public void NextStep() => _waitForDisplay = false;
        public void InteractHandler() => _waitToContinue = false;
    }
}