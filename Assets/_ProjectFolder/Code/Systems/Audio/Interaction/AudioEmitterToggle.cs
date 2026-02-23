using Unity.Mathematics;

namespace UnityEngine.Audio
{
    using UI;

    [RequireComponent(typeof(Toggle))]
    public class AudioEmitterToggle : AudioEmitter
    {
        [SerializeField] private float2 _pitch = Vector2.one;

        protected override void Awake()
        {
            base.Awake();
            Toggle button = GetComponent<Toggle>();

            switch (_type)
            {
                case ChannelType.Music: button.onValueChanged.AddListener(Play); break;
                case ChannelType.SoundFx: button.onValueChanged.AddListener(PlayOneShot); break;
            }
        }
        private void Play(bool value)
        {
            if (value) Play();
        }
        private void PlayOneShot(bool value)
        {
            if (value) PlayOneShot();
        }

        public override async void PlayOneShot()
        {
            var clip = await _manager.LoadAudioAsset(_audioReference, _isLoaded);
            _isLoaded = true;

            float pitch = _pitch.x == _pitch.y ? _pitch.x : Random.Range(_pitch.x, _pitch.y);
            if (!_overrideSource) { _manager.PlayOneShot(_type, clip, pitch); return; }

            _overrideSource.pitch = pitch;
            _overrideSource.PlayOneShot(clip);
        }
    }
}