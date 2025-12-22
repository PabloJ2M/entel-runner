using UnityEngine;
using UnityEngine.UI;

namespace Unity.SceneManagement
{
    [RequireComponent(typeof(Button))]
    public class SceneLoaderButton : SceneLoader
    {
        private void Start() => GetComponent<Button>().onClick.AddListener(SwipeScene);
    }
}