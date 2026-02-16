using UnityEngine;

namespace Gameplay.BossFight
{
    public class BossState_Begin : BossState
    {
        private const string _isAlive = "IsAlive";
        private const float _animationTotalTime = 1f;
        private float _animationDuration;

        public BossState_Begin(BossController boss) : base(boss) { }

        public override void Enter()
        {
            _animationDuration = _animationTotalTime;
            _boss.Animator.ResetControllerState();
            _boss.Animator.SetBool(_isAlive, true);
        }
        public override void Update()
        {
            _animationDuration -= Time.deltaTime;

            if (_animationDuration <= 0)
                _boss.TransitionToState(_boss.Atacks[0]);
        }
        public override void Exit()
        {
            
        }
    }
}