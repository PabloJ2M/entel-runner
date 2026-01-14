using System;
using UnityEngine;

namespace Unity.Achievements
{
    using Services.Economy;

    [Serializable] public class AchievementStatus
    {
        [SerializeField] private int _progress;
        [SerializeField] private int _target = 1;

        public bool isCompleted, hasPurchased;

        public float Percent => !isCompleted ? _progress / (float)_target : 1f;
        public string Text => $"{Percent * 100}%";//$"{_progress}/{_target}";
        public void Add(int amount) => _progress += amount;

        public AchievementStatus LoadJson(string id)
        {
            if (!PlayerPrefs.HasKey(id)) return this;

            string json = PlayerPrefs.GetString(id);
            return JsonUtility.FromJson<AchievementStatus>(json);
        }
        public void SaveJson(string id)
        {
            isCompleted = _progress >= _target;

            string json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(id, json);
        }
        public void ClearJson(string id)
        {
            _progress = 0;
            isCompleted = false;
            PlayerPrefs.DeleteKey(id);
        }
    }
    [Serializable] public struct AchievementRevenue
    {
        public BalanceType balance;
        public uint amount;
    }
}