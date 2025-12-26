using UnityEngine;

namespace UnityEngine.Audio
{
    public enum ChannelType
    {
        Music,
        SoundFx
    }
    public class AudioClipCache
    {
        public AudioClip clip;
        public byte refCount;
    }
}