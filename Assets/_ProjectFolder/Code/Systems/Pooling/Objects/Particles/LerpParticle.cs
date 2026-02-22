using System;
using UnityEngine;

namespace Unity.Pool
{
    public abstract class LerpParticle : PoolParticle
    {
        [SerializeField] private float _speed = 1f;

        protected Action onReachTarget = null;
        private Vector2 _target;
        private bool _isEnabled;

        protected virtual void Update()
        {
            if (!_isEnabled) return;
            Transform.position = Vector2.MoveTowards(Transform.position, _target, _speed * Time.deltaTime);

            if ((Vector2)Transform.position != _target) return;
            OnReachTarget();
            Destroy();
        }
        protected virtual void OnReachTarget()
        {
            onReachTarget?.Invoke();
            _isEnabled = false;
        }

        public virtual void SetPosition(Vector2 position, Vector2 target)
        {
            Transform.position = position;
            _target = target;
            _isEnabled = true;
        }
        public void SetActionAtTarget(Action action) => onReachTarget = action;
    }
}