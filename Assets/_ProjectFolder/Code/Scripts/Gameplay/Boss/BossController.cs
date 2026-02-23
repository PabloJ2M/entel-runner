using UnityEngine;

namespace Gameplay.BossFight
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private BossEncounter _encounter;
        [SerializeField] private Animator _animator;
        [SerializeField] private Health _health;

        private BossState _currentState;

        private BossState[] _atacks = new BossState[3];
        private BossState_Begin _introState;
        private BossState_Death _deadState;

        public float HealthPercent { get; private set; }
        public Animator Animator => _animator;
        public BossState[] Atacks => _atacks;

        private void Awake()
        {
            _introState = new(this);
            _deadState = new(this);
            _atacks[0] = new BossState_Atack1(this);
            _atacks[1] = new BossState_Atack2(this);
            _atacks[2] = new BossState_Atack3(this);
        }
        private void Update() => _currentState?.Update();
        private void OnEnable() => _health.onHealthUpdated += OnHealthUpdated;
        private void OnDisable() => _health.onHealthUpdated -= OnHealthUpdated;

        private void OnHealthUpdated(float value)
        {
            HealthPercent = value;
            if (HealthPercent <= 0) _encounter.ForceStop();
            //else _animator.Play("Hit");
        }

        public void StartFight()
        {
            _health?.ResetHealth();
            TransitionToState(_introState);
        }
        public void StopFight(bool defeated)
        {
            TransitionToState(_deadState);
        }

        public void TransitionToState(BossState state)
        {
            if (_currentState == state) return;
            _currentState?.Exit();
            
            _currentState = state;
            _currentState?.Enter();
        }
    }
}