using UnityEngine;

namespace Unity.Achievements
{
    public class AchievementTrigger : MonoBehaviour
    {
        [SerializeField] private SO_Achievement _reference;

        private void Start() => _reference.LoadData();
        public void AddProgress(int value) => _reference.Add(value);
    }
}