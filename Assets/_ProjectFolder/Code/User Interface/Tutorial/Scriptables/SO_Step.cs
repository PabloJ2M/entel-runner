using System;
using UnityEngine;

namespace Unity.Tutorial
{
    [CreateAssetMenu(fileName = "tutorial step", menuName = "tutorial/step", order = 1)]
    public class SO_Step : ScriptableObject
    {
        [SerializeField] private TutorialType _type;
        [SerializeField] private Dialogue[] _dialogues = new Dialogue[1];

        public TutorialType Type => _type;
        public Dialogue[] Dialogues => _dialogues;
        public IElement Element { get; set; }

        public Action onInteracted;
    }
}