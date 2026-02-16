using UnityEngine;

namespace Unity.Tutorial.UI
{
    public class MessagesDisplay : ElementBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField, Range(0f, 1f)] private float _textTime = 0.1f;
        [SerializeField] private DialogueUI _interface;

        private Dialogue[] _dialogues;
        private int _index;

        protected virtual void Start()
        {
            _container.SetActive(false);
        }

        protected override void OnStepStarted(SO_Step step)
        {
            _dialogues = step.Dialogues;
            _container.SetActive(true);
            DisplayDialogue();
        }
        protected override void OnStepCompleted()
        {
            _container.SetActive(false);
            _index = 0;
        }

        private void DisplayDialogue()
        {
            _interface.text?.SetText(_dialogues[_index].text);
            _interface.image?.SetSprite(_dialogues[_index].image);
            Invoke(nameof(NextDialogue), _textTime * _interface.text.text.Length);
        }

        public void NextDialogue()
        {
            _index++;
            if (_index < _dialogues.Length) DisplayDialogue();
            else _controller.NextStep();
        }
        public void SkipTime()
        {
            CancelInvoke();
            NextDialogue();
        }
    }
}