using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreUI;
    [SerializeField] private TextMeshProUGUI _highScoreUI;
    [SerializeField] private TweenCore _newScoreAnimation;

    private const string _highScore = "HighScore";
    public int Score { get; protected set; }

    private void Awake()
    {
        if (Application.isEditor)
            PlayerPrefs.DeleteKey(_highScore);
    }
    public void Add(int value) { Score += value; _scoreUI?.SetText(Score.ToString()); }
    public void Remove(int value) { Score -= value; _scoreUI?.SetText(Score.ToString()); }

    public void SaveNewScore()
    {
        int highScore = PlayerPrefs.GetInt(_highScore);

        if (Score > highScore)
        {
            highScore = Score;
            _newScoreAnimation?.Play(true);
            print("new score");
        }

        _highScoreUI?.SetText(Score.ToString());
        PlayerPrefs.SetInt(_highScore, highScore);
    }
}