using System.Threading.Tasks;
using System.Collections.Generic;

namespace UnityEngine.Audio
{
    using AddressableAssets;

    public class AudioManager : Singleton<AudioManager>, IAudioManager, IAudioSettings
    {
        [SerializeField] private AudioChannel[] _channels = {
            new(ChannelType.Music),
            new(ChannelType.SoundFx)
        };

        private Dictionary<ChannelType, AudioChannel> _channelMap = new();
        private Dictionary<AssetReference, AudioClipCache> _audioClipCache = new();

        protected override void Awake()
        {
            base.Awake();

            foreach (var channel in _channels)
            {
                channel.Init();
                _channelMap.Add(channel.Type, channel);
            }
        }

        public async Task<AudioClip> LoadAudioAsset(AssetReferenceT<AudioClip> reference, bool hasLoaded = false)
        {
            if (_audioClipCache.TryGetValue(reference, out var cache))
            {
                if (!hasLoaded) cache.refCount++;
                return cache.clip;
            }

            var clip = await reference.LoadAssetAsync().Task;
            _audioClipCache.Add(reference, new() { clip = clip, refCount = 1 });
            return clip;
        }
        public void UnloadAudioAsset(AssetReferenceT<AudioClip> reference)
        {
            if (!_audioClipCache.TryGetValue(reference, out var cache)) return;
            cache.refCount--;

            if (cache.refCount > 0) return;

            reference.ReleaseAsset();
            _audioClipCache.Remove(reference);
        }
        
        public void Play(ChannelType type, AudioClip resource) => _channelMap[type].Play(resource);
        public void PlayDefault(ChannelType type) => _channelMap[type].PlayDefault();
        public void PlayOneShot(ChannelType type, AudioClip clip, float pitch = 1f) => _channelMap[type].PlayOneShot(clip, pitch);

        public void SetVolume(ChannelType type, float volume) => _channelMap[type].SetVolume(volume);
        public void Mute(bool value)
        {
            foreach (var channel in _channels)
                channel.Mute(value);
        }
    }
}