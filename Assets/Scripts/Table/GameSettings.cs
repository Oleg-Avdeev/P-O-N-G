using UnityEngine;

namespace Pong.Game
{
    public sealed class GameSettings : MonoBehaviour
    {
        [SerializeField] private Material _ballMaterial = default;

        [SerializeField] private float _minBallSize = default;
        [SerializeField] private float _maxBallSize = default;

        [SerializeField] private float _minBallSpeed = default;
        [SerializeField] private float _maxBallSpeed = default;

        [SerializeField] private float _minSpeedFactor = default;
        [SerializeField] private float _maxSpeedFactor = default;

        public void Initialize()
        {
            var color = Data.DataManager.Instance.GetBallColor();
            _ballMaterial.SetColor("_Color", color);
        }

        public Ball.Parameters GetRandomBallParameters()
        {
            return new Ball.Parameters() {
                Size = Random.Range(_minBallSize, _maxBallSize),
                Speed = Random.Range(_minBallSpeed, _maxBallSpeed),
                SpeedUpFactor = Random.Range(_minSpeedFactor, _maxSpeedFactor),
            };
        }
    }
}