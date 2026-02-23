namespace UnityEngine.Audio
{
    public abstract class AudioEmitterBehaviour : MonoBehaviour
    {
        [SerializeField] protected ChannelType _type = ChannelType.SoundFx;

        protected IAudioManager _manager;
        protected bool _isLoaded;

        protected virtual void Awake() => _manager = AudioManager.Instance;

        public abstract void Play();
        public abstract void PlayOneShot();
        public void PlayDefault() => _manager?.PlayDefault(_type);
    }
}