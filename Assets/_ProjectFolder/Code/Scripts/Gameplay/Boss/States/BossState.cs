namespace Gameplay.BossFight
{
    public abstract class BossState
    {
        protected BossController _boss;

        protected BossState(BossController boss) => this._boss = boss;

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}