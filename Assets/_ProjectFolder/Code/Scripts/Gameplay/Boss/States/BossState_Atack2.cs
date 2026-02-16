using System.Collections;

namespace Gameplay.BossFight
{
    public class BossState_Atack2 : BossState_Atack
    {
        protected override float _healthHandler => 0.3f;
        protected override int _nextAtackIndex => 2;

        public BossState_Atack2(BossController boss) : base(boss) { }

        protected override IEnumerator AtackEvent()
        {
            yield return null;
        }
    }
}