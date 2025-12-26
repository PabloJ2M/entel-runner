
namespace UnityEngine.Audio
{
    using AddressableAssets;

    public class AudioEmitter : AudioEmitterBehaviour
    {
        [Header("Audio Reference")]
        [SerializeField] protected AssetReferenceT<AudioClip> _audioReference;

        private async void Start()
        {
            if (!_preloadOnStart) return;
            await _manager.LoadAudioAsset(_audioReference);
            _isLoaded = true;
        }
        private void OnDestroy()
        {
            if (!_isLoaded) return;
            _manager.UnloadAudioAsset(_audioReference);
        }

        public override async void Play()
        {
            var resource = await _manager.LoadAudioAsset(_audioReference, _isLoaded);
            _isLoaded = true;

            if (!_overrideSource)
            {
                _manager.Play(_type, resource);
                return;
            }

            _overrideSource.resource = resource;
            _overrideSource.Play();
        }
        public override async void PlayOneShot()
        {
            var clip = await _manager.LoadAudioAsset(_audioReference, _isLoaded);
            _isLoaded = true;

            if (!_overrideSource) _manager.PlayOneShot(_type, clip);
            else _overrideSource.PlayOneShot(clip);
        }
    }
}