using System.Collections;
using UnityEngine;
using Unity.Pool;

namespace Gameplay.BossFight
{
    public class BossState_Atack1 : BossState_Atack
    {
        protected override float _healthHandler => 0.6f;
        protected override int _nextAtackIndex => 1;

        private PoolManagerObjectsPattern _spawner;
        private WaitForSeconds _spawnRate;

        private const string _projectilePrefabName = "boss projectile";
        private const string _knockbackPrefabName = "wifi";

        public BossState_Atack1(BossController boss) : base(boss)
        {
            _spawner = Object.FindFirstObjectByType<PoolManagerObjectsPattern>();
            _spawnRate = new(1f);
        }

        protected override IEnumerator AtackEvent()
        {
            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return _spawnRate;
                    _boss.Animator.SetTrigger(_atackTriggerAnimation);
                    _spawner.SpawnInRandomPath(_projectilePrefabName);
                }

                yield return _spawnRate;
                _boss.Animator.SetTrigger(_atackTriggerAnimation);
                _spawner.SpawnInRandomPath(_knockbackPrefabName);
            }
        }
    }
}