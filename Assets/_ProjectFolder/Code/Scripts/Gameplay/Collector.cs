using UnityEngine;
using UnityEngine.Audio;
using Unity.Achievements;

public class Collector : AudioEmitter
{
    [SerializeField] private UIScore _score;
    [SerializeField] private AchievementTrigger _trigger;

    public void AddPoints(int value)
    {
        PlayOneShot();
        _score.Add(value);
        _trigger.AddProgress(value);
    }
}