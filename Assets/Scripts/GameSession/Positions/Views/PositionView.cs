using System;
using UnityEngine;
using Zenject;

namespace Elements.GameSession.Positions.Views
{
    public class PositionView : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        public event Action OnClick;

        public Vector3 Position => transform.position;

        private IMemoryPool _pool;

        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }

        public void SetPosition(Vector3 position)
        {
            transform.localPosition = position;
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