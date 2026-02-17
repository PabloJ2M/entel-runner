namespace Gameplay.Events
{
    public interface IGameEvent
    {
        void OnStartEvent();
        void OnCompleteEvent(bool success);
    }
}