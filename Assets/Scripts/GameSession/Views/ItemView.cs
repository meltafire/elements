using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Elements.GameSession.Views
{
    public class ItemView : MonoBehaviour
    {
        private bool _isMovementRequired = false;
        private Vector3 _movementTarget;

        private void Update()
        {
            if (_isMovementRequired)
            {
                transform.position = Vector3.MoveTowards(transform.position, _movementTarget, Time.deltaTime * 1);
            }
        }

        public async UniTask MoveToPosition(Vector3 position, CancellationToken token)
        {
            _movementTarget = position;
            _isMovementRequired = true;

            await UniTask.WaitUntil(CheckMovementCondition, PlayerLoopTiming.Update, token);

            _isMovementRequired = false;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private bool CheckMovementCondition()
        {
            return transform.position == _movementTarget;
        }
    }
}