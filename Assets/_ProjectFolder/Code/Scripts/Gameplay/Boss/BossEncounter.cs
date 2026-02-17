using UnityEngine;

namespace Gameplay.BossFight
{
    using Events;

    [RequireComponent(typeof(BossController))]
    public class BossEncounter : MonoBehaviour, IGameEvent
    {
        [SerializeField] private GameEventListener _event;
        [SerializeField] private float _duration = 18f;

        private BossController _boss;
        private bool _running;
        private float _timer;

        private void Awake() => _boss = GetComponent<BossController>();
        private void Update()
        {
            if (!_running) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0f) _event.CompleteEvent(false);
        }

        public void ForceStop()
        {
            _event.CompleteEvent(true);
        }
        public void OnStartEvent()
        {
            _running = true;
            _timer = _duration;
            _boss.StartFight();
        }
        public void OnCompleteEvent(bool success)
        {
            _running = false;
            _boss.StopFight(success);
        }
    }
}