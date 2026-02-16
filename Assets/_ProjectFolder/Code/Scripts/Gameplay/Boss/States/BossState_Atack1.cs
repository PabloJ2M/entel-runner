using System.Collections;

namespace Gameplay.BossFight
{
    public class BossState_Atack1 : BossState_Atack
    {
        protected override float _healthHandler => 0.6f;
        protected override int _nextAtackIndex => 1;

        public BossState_Atack1(BossController boss) : base(boss) { }

        protected override IEnumerator AtackEvent()
        {
            yield return null;
        }
    }
}