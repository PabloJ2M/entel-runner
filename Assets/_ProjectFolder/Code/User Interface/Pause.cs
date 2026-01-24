using UnityEngine;
using UnityEngine.Animations;

namespace Gameplay
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private TweenCanvasGroup _pauseScreen;

        public void ButtonPause()
        {
            _pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        public void ButtonContinue()
        {
            _pauseScreen.FadeOut();
            Time.timeScale = 1f;
        }
    }
}