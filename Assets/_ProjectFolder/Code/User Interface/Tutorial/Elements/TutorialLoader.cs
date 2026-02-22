using System.Collections;
using UnityEngine;

namespace Unity.Tutorial
{
    using SceneManagement;

    public class TutorialLoader : MonoBehaviour
    {
        [SerializeField] private GameplayManager _manager;
        [SerializeField] private SceneLoader _loader;

        private const string _tutorial = "Tutorial";

        private async void Start()
        {
            await Awaitable.EndOfFrameAsync();
            if (PlayerPrefs.HasKey(_tutorial)) return;
            _manager.Pause();
            _loader.AddScene();
            StartCoroutine(WaitForTutorial());
        }

        private IEnumerator WaitForTutorial()
        {
            yield return new WaitUntil(() => TutorialManager.Instance);
            TutorialManager.Instance.OnTutorialCompleted += OnTutorialCompleted;
        }
        private void OnTutorialCompleted()
        {
            PlayerPrefs.SetInt(_tutorial, 1);
            _manager.Play();
            _loader.RemoveScene();
        }
    }
}