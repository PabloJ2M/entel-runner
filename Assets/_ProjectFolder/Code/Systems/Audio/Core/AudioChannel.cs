using System;

namespace UnityEngine.Audio
{
    [Serializable] public class AudioChannel
    {
        [SerializeField] private string _mixerParam;
        [SerializeField] private ChannelType _type;
        [SerializeField] private AudioSource _source;

        private AudioMixerGroup _group;
        private AudioClip _defaultClip;

        public ChannelType Type => _type;

        public AudioChannel(ChannelType type)
        {
            _mixerParam = _type.ToString();
            _type = type;
        }
        public void Init()
        {
            _group = _source.outputAudioMixerGroup;
            _defaultClip = _source.clip;
        }

        public void Mute(bool value) => _source.mute = value;
        public void SetVolume(float value)
        {
            float dB = Mathf.Log10(Mathf.Max(0.001f, value)) * 20f;
            _group.audioMixer.SetFloat(_mixerParam, dB);
        }
        public float GetVolume()
        {
            _group.audioMixer.GetFloat(_mixerParam, out float dB);
            return Mathf.Pow(10f, dB / 20f);
        }

        public void Play(AudioClip clip)
        {
            if (_source.clip == clip) return;
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