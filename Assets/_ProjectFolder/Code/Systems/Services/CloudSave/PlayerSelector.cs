using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] _options;

    private static uint _selected = 0;

    private void Start() => SetIndex();
    private void SetIndex()
    {
        for (int i = 0; i < _options.Length; i++)
            _options[i].SetActive(i == _selected);
    }

    public void ChangeCharacter()
    {
        _selected++;
        _selected %= (uint)_options.Length;
        SetIndex();
    }
}