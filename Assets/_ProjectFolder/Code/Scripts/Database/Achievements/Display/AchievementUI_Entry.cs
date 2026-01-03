using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Unity.Achievements
{
    using Pool;

    public class AchievementUI_Entry : PoolObjectBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        
        [Header("Percentage")]
        [SerializeField] private Image _fillAmount;
        [SerializeField] private TextMeshProUGUI _textCount;

        [Header("Reward Button")]
        [SerializeField] private Button _claimButton;

        private SO_Achievement _achievement;

        private void Awake() => _claimButton.onClick.AddListener(ClaimReward);

        public void Init(SO_Achievement item)
        {
            _achievement = item;
            _achievement.LoadProgress();

            _name?.SetText(_achievement.Name);
            _description?.SetText(_achievement.Description);

            _fillAmount.fillAmount = _achievement.Status.Percent;
            _textCount?.SetText(_achievement.Status.Text);

            _claimButton.interactable = _achievement.Status.isCompleted || _achievement.Status.hasPurchased;
        }

        private void ClaimReward()
        {
            GetComponentInParent<AchievementUI>().ClaimReward(_achievement.Revenue);

            _achievement.Status.hasPurchased = true;
            _achievement.SaveProgress();

            _claimButton.interactable = false;
        }
    }
}