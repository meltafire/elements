using Elements.Balloons.Data;
using System;
using UnityEngine;
using Zenject;

namespace Elements.Balloons.Views
{
    public class BalloonView : MonoBehaviour, IPoolable<BalloonData, IMemoryPool>, IDisposable
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public event Action<BalloonView> OnBecameInvisible;

        private IMemoryPool _pool;
        private float _sinModifier;
        private float _speed;
        private bool _startCheckVisibility;

        private void Update()
        {
            Move();
            CheckVisibility();
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(BalloonData data, IMemoryPool pool)
        {
            SetSprite(data.Sprite);

            _sinModifier = data.SinModifier;
            _speed = data.Speed;
            transform.localPosition = data.StartPosition;
            _pool = pool;
        }

        private void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        private void Move()
        {
            var position = transform.localPosition;
            var nextPositionX = position.x + _speed * Time.deltaTime;

            transform.localPosition = new Vector3(
                nextPositionX,
                Mathf.Sin(nextPositionX) * _sinModifier,
                position.z);
        }

        private void CheckVisibility()
        {
            //TODO: Out of frustum check. Unexpected behaviour: First _spriteRenderer.isVisible check is always false After OnSpawned
            if (_spriteRenderer.isVisible)
            {
                _startCheckVisibility = true;
            }
            else if (_startCheckVisibility)
            {
                _startCheckVisibility = false;
                OnBecameInvisible?.Invoke(this);
            }
        }
    }
}