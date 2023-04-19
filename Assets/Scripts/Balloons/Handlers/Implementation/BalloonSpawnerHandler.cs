using Elements.Balloons.Data;
using Elements.Balloons.Factories;
using Elements.Balloons.Handlers.Infrastructure;
using Elements.Balloons.Views;
using UnityEngine;

namespace Elements.Balloons.Handlers.Implementation
{
    public class BalloonSpawnerHandler : IBalloonSpawnerHandler
    {
        private readonly BalloonViewFactory _viewFactory;
        private readonly BalloonsSettings _balloonsSettings;

        private Vector3 _lowerLeftCornerInUnits;
        private Vector3 _upperRightInUnits;
        private float _depthStepZ;

        public BalloonSpawnerHandler(BalloonViewFactory viewFactory, BalloonsSettings balloonsSettings)
        {
            _viewFactory = viewFactory;
            _balloonsSettings = balloonsSettings;
        }

        public BalloonView SpawnBalloon(int balloonCount)
        {
            var balloonData = GenerateBalloonData(balloonCount);

            return _viewFactory.Create(balloonData);
        }

        public void Initialize()
        {
            var camera = Camera.main; 

            var lowerCenterInScreenPoint = camera.WorldToScreenPoint(new Vector2(0, _balloonsSettings.LowerY));
            var upperCenterInScreenPoint = camera.WorldToScreenPoint(new Vector2(0, _balloonsSettings.UpperY));
            _lowerLeftCornerInUnits = camera.ScreenToWorldPoint(new Vector2(0, lowerCenterInScreenPoint.y));
            _upperRightInUnits = camera.ScreenToWorldPoint(new Vector2(Screen.width, upperCenterInScreenPoint.y));

            _depthStepZ = _balloonsSettings.DepthDelta / _balloonsSettings.MaxBalloonOnScreen;
        }

        private BalloonData GenerateBalloonData(int balloonCount)
        {
            var sprites = _balloonsSettings.Sprites;
            var spriteIndex = Random.Range(0, sprites.Length);
            var sprite = sprites[spriteIndex];

            var sinModifier = Random.Range(_balloonsSettings.SinAmpMin, _balloonsSettings.SinAmpMax);

            var isLeftSpawn = Random.Range(0, 2) == 0;

            var speed = Random.Range(_balloonsSettings.SpeedMin, _balloonsSettings.SpeedMax);
            if (!isLeftSpawn)
            {
                speed *= -1;
            }

            var halfOfSpriteInUnitsX = sprite.bounds.size.x / 2;

            var spawnY = Random.Range(_lowerLeftCornerInUnits.y, _upperRightInUnits.y);
            var spawnX = isLeftSpawn ? _lowerLeftCornerInUnits.x - halfOfSpriteInUnitsX : _upperRightInUnits.x + halfOfSpriteInUnitsX;
            var spawnZ = -_depthStepZ * balloonCount;

            var spawnPosition = new Vector3(
                spawnX,
                spawnY,
                spawnZ);

            return new BalloonData(
                sprite,
                sinModifier,
                speed,
                spawnPosition
                );
        }
    }
}