using UnityEngine.AddressableAssets;

namespace UnityEngine.Audio
{
    public interface IAudioManager
    {
        Awaitable<AudioClip> LoadAudioAsset(AssetReferenceT<AudioClip> reference, bool hasLoaded = false);
        void UnloadAudioAsset(AssetReferenceT<AudioClip> reference);

        void Play(ChannelType type, AudioClip key);
        void PlayDefault(ChannelType type);
        void PlayOneShot(ChannelType type, AudioClip key, float pitch = 1f);
    }
    public interface IAudioSettings
    {
        void SetVolume(ChannelType type, float volume);
        float GetVolume(ChannelType type);
        void Mute(bool value);
    }
}