using System.Collections;
using Unity.Services;
using Unity.Services.Economy;
using UnityEngine;
using TMPro;

public class CoinsConversion : MonoBehaviour
{
    [SerializeField] private UIScore _scoreUI;
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private int _pointsPerAward;

    public void ConvertScore()
    {
        int revenue = _scoreUI.Score / _pointsPerAward;
        _textDisplay.SetText("0");

        if (revenue <= 0) return;

        var economy = UnityServiceInit.Instance?.GetComponentInChildren<PlayerEconomyService>();
        economy.AddBalanceID(BalanceType.COIN, (uint)revenue);

        StartCoroutine(TextAnimation(revenue));
    }
    private IEnumerator TextAnimation(int target)
    {
        yield return new WaitForSeconds(1f);
        float n = 0;

        while (n < target)
        {
            n = Mathf.Lerp(n, target + 1, Time.deltaTime * 5f);
            _textDisplay?.SetText(n.ToString("0"));
            yield return null;
        }
    }
}