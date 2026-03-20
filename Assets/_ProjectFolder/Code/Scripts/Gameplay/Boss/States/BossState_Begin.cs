using PrimeTween;

namespace Gameplay.BossFight
{
    public class BossState_Begin : BossState
    {
        public BossState_Begin(BossController boss) : base(boss) { }

        public override void Enter()
        {
            Tween.LocalPositionX(_boss.transform, -9f, 1f).OnComplete(CompleteAction);
            void CompleteAction() => _boss.TransitionToState(_boss.Atacks[0]);
        }
        public override void Update() { }
        public override void Exit() { }
    }
}