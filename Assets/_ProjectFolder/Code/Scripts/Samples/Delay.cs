using System.Collections;

namespace UnityEngine.Events
{
    public class Delay : MonoBehaviour
    {
        [SerializeField] private bool _playOnAwake;
        [SerializeField] private float _time;
        [SerializeField] private UnityEvent _onCompleteDelay;

        private WaitForSeconds _seconds;

        private void Awake() => _seconds = new(_time);
        private void Start() { if (_playOnAwake) StartCoroutine(StartDelay()); }

        public void Play() => StartDelay();
        public void Cancel() => StopAllCoroutines();

        private IEnumerator StartDelay()
        {
            yield return _seconds;
            _onCompleteDelay.Invoke();
        }
    }
}