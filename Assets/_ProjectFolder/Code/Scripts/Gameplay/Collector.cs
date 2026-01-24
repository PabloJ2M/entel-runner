using Unity.Achievements;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private UIScore _score;
    [SerializeField] private AchievementTrigger _trigger;

    public void AddPoints(int value)
    {
        _score.Add(value);
        _trigger.AddProgress(value);
    }
}