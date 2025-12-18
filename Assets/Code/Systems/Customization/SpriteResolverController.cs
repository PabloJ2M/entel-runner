using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(SpriteLibrary))]
public class SpriteResolverController : MonoBehaviour
{
    private SpriteLibrary _library;
    private Dictionary<string, SpriteResolverElement> _resolvers;

    private const string _default = "default";

    private void Awake() => _library = GetComponent<SpriteLibrary>();
    private void Start()
    {
        foreach (string category in _library.spriteLibraryAsset.GetCategoryNames())
            SetLabel(category, PlayerPrefs.GetString(category, _default));
    }

    public void AddListener(string category, SpriteResolverElement element) => _resolvers.Add(category, element);
    public void RemoveListener(string category) => _resolvers.Remove(category);

    public void SetLabel(string category, string label)
    {
        _resolvers[category].SetLabel(label);
        PlayerPrefs.SetString(category, label);
    }
}