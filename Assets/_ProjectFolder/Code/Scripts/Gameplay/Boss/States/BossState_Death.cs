using PrimeTween;

namespace Gameplay.BossFight
{
    public class BossState_Death : BossState
    {
        public BossState_Death(BossController boss) : base(boss) { }

        public override void Enter() => Tween.LocalPositionX(_boss.transform, 0, 1f);
        public override void Exit()
        {
            
        }
        public override void Update()
        {
            
        }
    }
}