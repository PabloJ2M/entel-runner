namespace UnityEngine.Audio
{
    public class AudioTransition : AudioEmitterBehaviour
    {
        private IAudioSettings _audioSettings;
        private CanvasGroup _group;
        private float _volume;

        protected override void Awake()
        {
            base.Awake();
            _group = GetComponent<CanvasGroup>();
            _audioSettings = AudioManager.Instance;
            _volume = _audioSettings.GetVolume(_type);
        }
        private void Update()
        {
            _audioSettings.SetVolume(_type, _group.alpha / _volume);
        }

        public override void Play() { }
        public override void PlayOneShot() { }
    }
}