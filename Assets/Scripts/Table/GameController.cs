using Pong.Game.PaddleControllers;
using Pong.Game.UI;
using UnityEngine;

namespace Pong.Game
{
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private Camera _gameCamera = default;
        [SerializeField] private CountIn _countInUI = default;
        [SerializeField] private Score _scoreUI = default;

        [SerializeField] private Ball _ball = default;
        [SerializeField] private float _minBallSize = default;
        [SerializeField] private float _maxBallSize = default;
        [SerializeField] private float _minBallSpeed = default;
        [SerializeField] private float _maxBallSpeed = default;

        [SerializeField] private Paddle _paddle1 = default;
        [SerializeField] private Paddle _paddle2 = default;

        private int _scoreBottom = 0;
        private int _scoreTop = 0;
        private bool _running = false;
        private (int top, int bottom) _bestScore;

        public void StartGame()
        {
            _paddle1.SetController(new PlayerController(isBottomPlayer: true, camera: _gameCamera));
            _paddle2.SetController(new AIController(_ball));
            
            _bestScore = Data.DataManager.Instance.GetBestScore();
            _scoreUI.SetBestScore(_bestScore.top, _bestScore.bottom);
            _scoreUI.SetScore(0, 0);

            // _paddle2.SetController(new PlayerController(isBottomPlayer: false, camera: _gameCamera));

            StartRound();
        }

        public void QuitGame()
        {
            if (_scoreBottom + _scoreTop > _bestScore.bottom + _bestScore.top)
            {
                Data.DataManager.Instance.SetBestScore(_scoreBottom, _scoreTop);
            }
        }

        public void StartRound()
        {
            var p = new Ball.Parameters() {
                Size = Random.Range(_minBallSize, _maxBallSize),
                Speed = Random.Range(_minBallSpeed, _maxBallSpeed),
                SpeedUpFactor = 1.1f
            };

            _ball.Appear(p);

            _countInUI.Show(seconds: 3, onComplete: () => {
                _running = true;
                _ball.Activate();
            });
        }

        private void FixedUpdate()
        {
            if (_running)
            {
                float y = _ball.GetPosition.y;

                if (Mathf.Abs(y) > 10)
                {
                    _running = false;
                    _ball.Deactivate();

                    if (y > 0) _scoreBottom++;
                    else _scoreTop++;

                    _scoreUI.SetScore(_scoreTop, _scoreBottom);

                    _ball.Disappear(StartRound);
                }
            }
        }
    }
}