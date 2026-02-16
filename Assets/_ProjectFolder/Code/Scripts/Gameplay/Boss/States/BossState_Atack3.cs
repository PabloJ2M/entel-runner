using System.Collections;

namespace Gameplay.BossFight
{
    public class BossState_Atack3 : BossState_Atack
    {
        protected override float _healthHandler => 0;
        protected override int _nextAtackIndex => 0;

        public BossState_Atack3(BossController boss) : base(boss) { }

        public override void Update() { }

        protected override IEnumerator AtackEvent()
        {
            yield return null;
        }
    }
}