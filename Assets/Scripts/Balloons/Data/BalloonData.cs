using UnityEngine;

namespace Elements.Balloons.Data
{
    public class BalloonData
    {
        public readonly Sprite Sprite;
        public readonly float SinModifier;
        public readonly float Speed;
        public readonly Vector3 StartPosition;

        public BalloonData(Sprite sprite, float sinModifier, float speed, Vector3 startPosition)
        {
            Sprite = sprite;
            SinModifier = sinModifier;
            Speed = speed;
            StartPosition = startPosition;
        }
    }
}