using Cysharp.Threading.Tasks;
using Elements.Balloons.Data;
using Elements.Balloons.Handlers.Infrastructure;
using Elements.Balloons.Views;
using Elements.Tools;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Elements.Balloons.Controllers
{
    public class BalloonController
    {
        private readonly IBalloonSpawnerHandler _spawnerHandler;
        private readonly BalloonsSettings _balloonsSettings;
        private readonly List<BalloonView> _views;

        private int _balloonCount;

        public BalloonController(IBalloonSpawnerHandler spawnerHandler, BalloonsSettings balloonsSettings)
        {
            _spawnerHandler = spawnerHandler;
            _balloonsSettings = balloonsSettings;
            _views = new List<BalloonView>();
        }

        public async UniTask Execute(CancellationToken token)
        {
            _spawnerHandler.Initialize();

            while (!token.IsCancellationRequested)
            {
                await UniTask.WaitWhile(IsSpawnLimited, PlayerLoopTiming.Update, token);

                if (token.IsCancellationRequested)
                {
                    break;
                }

                var timeUntielSpawnMs = (int)(Random.Range(0f, _balloonsSettings.MaxSecondsBetweenSpawn) * Constants.MillisecondsInSeconds);

                await UniTask.Delay(timeUntielSpawnMs, false, PlayerLoopTiming.Update, token);

                if (token.IsCancellationRequested)
                {
                    break;
                }

                _balloonCount++;
                var view = _spawnerHandler.SpawnBalloon(_balloonCount);

                view.OnBecameInvisible += OnBecameInvisible;
            }

            DespawnViews();
        }

        private bool IsSpawnLimited()
        {
            return _balloonCount >= _balloonsSettings.MaxBalloonOnScreen;
        }

        private void OnBecameInvisible(BalloonView view)
        {
            _views.Remove(view);
            DespawnView(view);
        }

        private void DespawnViews()
        {
            foreach (var view in _views)
            {
                DespawnView(view);
            }

            _views.Clear();
        }

        private void DespawnView(BalloonView view)
        {
            view.OnBecameInvisible -= OnBecameInvisible;
            view.Dispose();
            _balloonCount--;
        }
    }
}