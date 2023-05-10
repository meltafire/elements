using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Elements.GameSession.Views
{
    public class ItemView : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField]
        private Animator _animator;

        public Animator Animator => _animator;

        private bool _isMovementRequired = false;
        private Vector3 _movementTarget;
        private float _speed;
        private IMemoryPool _pool;

        private void Update()
        {
            if (_isMovementRequired)
            {
                transform.position = Vector3.MoveTowards(transform.position, _movementTarget, Time.deltaTime * _speed);
            }
        }

        public async UniTask MoveToPosition(Vector3 position, float speed, CancellationToken token)
        {
            _movementTarget = position;
            _isMovementRequired = true;
            _speed = speed;

            await UniTask.WaitUntil(CheckMovementCondition, PlayerLoopTiming.Update, token);

            _isMovementRequired = false;
        }

        public void PlayAnimation(int trigger)
        {
            _animator.SetTrigger(trigger);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private bool CheckMovementCondition()
        {
            return transform.position == _movementTarget;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
    }
}