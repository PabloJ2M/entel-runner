using System.Collections;
using UnityEngine;

namespace Gameplay.BossFight
{
    public class BossManager : GameplayListener
    {
        [SerializeField] private float _spawnDelay = 900f;

        private float _traveled;

        protected override void OnEnable()
        {
            base.OnEnable();
            _gameManager.onGameStarted.AddListener(StartDistance);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _gameManager.onGameStarted.RemoveListener(StartDistance);
        }
        private void StartDistance() => _traveled = _gameManager.WorldDistance;

        protected override void GameUpdate(float traveled)
        {
            if (!_gameManager.IsEnabled) return;
            if (traveled - _traveled < _spawnDelay) return;

            SetStatus(true);
            StartCoroutine(BossLoop());
        }

        private IEnumerator BossLoop()
        {
            yield return new WaitForSeconds(10f);

            _traveled = _gameManager.WorldDistance;
            SetStatus(false);
        }
        private void SetStatus(bool value)
        {
            if (value) _gameManager.Pause();
            else _gameManager.Play();
        }
    }
}