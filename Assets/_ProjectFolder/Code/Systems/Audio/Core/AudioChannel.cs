using System;

namespace UnityEngine.Audio
{
    [Serializable] public class AudioChannel
    {
        [SerializeField] private ChannelType _type;
        [SerializeField] private AudioSource _source;
        [SerializeField] private string _mixerParam;

        private AudioMixerGroup _group;
        private AudioClip _defaultClip;

        public ChannelType Type => _type;

        public AudioChannel(ChannelType type)
        {
            _type = type;
            _mixerParam = _type.ToString();
        }
        public void Init()
        {
            _group = _source.outputAudioMixerGroup;
            _defaultClip = _source.clip;
        }

        public void SetVolume(float value) => _group.audioMixer.SetFloat(_mixerParam, Mathf.Log10(Mathf.Max(0.001f, value)) * 20f);
        public void Mute(bool value) => _source.mute = value;

        public void Play(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }
        public void PlayOneShot(AudioClip clip, float pitch = 1f)
        {
            _source.pitch = pitch;
            _source?.PlayOneShot(clip);
        }
        public void PlayDefault() => Play(_defaultClip);
    }
}