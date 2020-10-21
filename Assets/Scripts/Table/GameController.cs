using Pong.Game.PaddleControllers;
using Pong.Game.UI;
using UnityEngine;

namespace Pong.Game
{
    public enum GameType { PvE, Local, Remote }

    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings = default;
        [SerializeField] private CountIn _countInUI = default;
        [SerializeField] private Score _scoreUI = default;

        [SerializeField] private Ball _ball = default;

        [SerializeField] private PaddleControllerFactory _paddleControllerFactory = default;
        [SerializeField] private Paddle _paddle1 = default;
        [SerializeField] private Paddle _paddle2 = default;

        private int _scoreBottom = 0;
        private int _scoreTop = 0;
        private bool _running = false;
        private (int top, int bottom) _bestScore;

        public void StartGame(GameType gameType)
        {
            _gameSettings.Initialize();
            InitializePaddles(gameType);

            _bestScore = Data.DataManager.Instance.GetBestScore();
            _scoreUI.SetBestScore(_bestScore.top, _bestScore.bottom);
            _scoreUI.SetScore(0, 0);

            StartRound();
        }

        private void InitializePaddles(GameType gameType)
        {
            if (gameType == GameType.Local)
            {
                _paddle1.SetController(_paddleControllerFactory.GetController(PaddleType.Player, true));
                _paddle2.SetController(_paddleControllerFactory.GetController(PaddleType.Player, false));
            }
            else if (gameType == GameType.PvE)
            {
                _paddle1.SetController(_paddleControllerFactory.GetController(PaddleType.Player, true));
                _paddle2.SetController(_paddleControllerFactory.GetController(PaddleType.Player, false));
            }
            else if (gameType == GameType.Remote)
            {
                _paddle1.SetController(_paddleControllerFactory.GetController(PaddleType.Player, true));
                _paddle2.SetController(_paddleControllerFactory.GetController(PaddleType.AI, false));
            }
        }

        public void QuitGame()
        {
            _ball.Deactivate();
            _ball.Disappear();
            _running = false;
        }

        public void StartRound()
        {
            _ball.Appear(_gameSettings.GetRandomBallParameters());

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
                    _ball.Disappear(StartRound);
                    Score(y > 0);
                }
            }
        }

        private void Score(bool bottom)
        {

            if (bottom) _scoreBottom++;
            else _scoreTop++;

            _scoreUI.SetScore(_scoreTop, _scoreBottom);
            if (_scoreBottom + _scoreTop > _bestScore.bottom + _bestScore.top)
            {
                Data.DataManager.Instance.SetBestScore(_scoreBottom, _scoreTop);
            }
        }
    }
}