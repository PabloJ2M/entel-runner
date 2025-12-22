using UnityEngine;

namespace Unity.SceneManagement
{
    public abstract class SceneLoader : MonoBehaviour
    {
        [SerializeField, Scene] private string _path;

        protected void SwipeScene() => SceneController.Instance.ChangeScene(_path);
    }
}