namespace Gameplay.BossFight
{
    public class BossState_Death : BossState
    {
        private const string _isAlive = "IsAlive";

        public BossState_Death(BossController boss) : base(boss) { }

        public override void Enter() => _boss.Animator.SetBool(_isAlive, false);
        public override void Exit()
        {
            
        }
        public override void Update()
        {
            
        }
    }
}