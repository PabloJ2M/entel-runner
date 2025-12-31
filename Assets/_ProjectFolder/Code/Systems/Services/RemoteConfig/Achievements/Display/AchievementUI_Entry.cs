using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace Unity.Achievements
{
    using Pool;

    public class AchievementUI_Entry : PoolObjectBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private UnityEvent<float> _onProgressUpdated;

        public void Init(SO_Achievement reference)
        {
            reference.LoadData();

            _name?.SetText(reference.Name);
            _description?.SetText(reference.Description);
            _onProgressUpdated.Invoke(reference.Progress());
        }

        public void ClaimReward()
        {
            
        }
    }
}