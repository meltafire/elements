using UnityEngine;

namespace Elements.Balloons.Data
{
    [CreateAssetMenu(fileName = "Balloons Settings", menuName = "elements game/Balloons Settings", order = 1)]
    public class BalloonsSettings : ScriptableObject
    {
        [SerializeField]
        private int _maxBalloonOnScreen;
        [SerializeField]
        private float _maxSecondsBetweenSpawn;
        [SerializeField]
        private float _upperY;
        [SerializeField]
        private float _lowerY;
        [SerializeField]
        private float _sinAmpMax;
        [SerializeField]
        private float _sinAmpMin;
        [SerializeField]
        private float _speedMin;
        [SerializeField]
        private float _speedMax;
        [SerializeField]
        private float _depthDelta;
        [SerializeField]
        private Sprite[] _sprites;

        public int MaxBalloonOnScreen => _maxBalloonOnScreen;
        public float MaxSecondsBetweenSpawn => _maxSecondsBetweenSpawn;
        public float UpperY => _upperY;
        public float LowerY => _lowerY;
        public float SinAmpMax => _sinAmpMax;
        public float SinAmpMin => _sinAmpMin;
        public float SpeedMin => _speedMin;
        public float SpeedMax => _speedMax;
        public float DepthDelta => _depthDelta;
        public Sprite[] Sprites => _sprites;
    }
}