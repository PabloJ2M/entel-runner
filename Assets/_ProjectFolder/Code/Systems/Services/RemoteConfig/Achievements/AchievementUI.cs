using UnityEngine;
using UnityEngine.Events;

namespace Unity.Achievements
{
    public class AchievementUI : MonoBehaviour
    {
        [SerializeField] private SO_Achievement _reference;
        [SerializeField] private UnityEvent<float> _onProgressUpdated;

        private void Start()
        {
            _reference.LoadData();
            _onProgressUpdated.Invoke(_reference.Progress());
        }
    }
}