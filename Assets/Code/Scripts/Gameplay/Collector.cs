using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private UIScore _score;

    public void AddPoints(int value) => _score.Add(value);
}