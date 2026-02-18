using System.Collections;
using UnityEngine;

namespace Gameplay.BossFight
{
    public abstract class BossState_Atack : BossState
    {
        protected abstract float _healthHandler { get; }
        protected abstract int _nextAtackIndex { get; }
        
        protected Coroutine _coroutine;
        protected const string _atackTriggerAnimation = "Atack";

        public BossState_Atack(BossController boss) : base(boss) { }

        public override void Enter() => _coroutine = _boss.StartCoroutine(AtackEvent());
        public override void Exit() => _boss.StopCoroutine(_coroutine);
        public override void Update()
        {
            if (_boss.HealthPercent <= _healthHandler)
                _boss.TransitionToState(_boss.Atacks[_nextAtackIndex]);
        }

        protected abstract IEnumerator AtackEvent();
    }
}