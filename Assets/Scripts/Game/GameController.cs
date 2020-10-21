using Pong.Game.PaddleControllers;
using Pong.Game.Network;
using Pong.Game.UI;
using UnityEngine;

namespace Pong.Game
{
    public enum GameType { PvE, Local, Remote }

    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private PaddlesController _paddlesController = default;
        [SerializeField] private GameSettings _gameSettings = default;
        [SerializeField] private CountIn _countInUI = default;
        [SerializeField] private Score _scoreUI = default;
        [SerializeField] private Ball _ball = default;

        private bool _running = false;
        private bool _waiting = false;
        private bool _remote = false;
        private (int top, int bottom) _bestScore;

        public void StartGame(GameType gameType)
        {
            Debug.LogWarning($"Starting game {gameType}");
            _gameSettings.Initialize();

            _bestScore = Data.DataManager.Instance.GetBestScore();
            _scoreUI.SetBestScore(_bestScore.top, _bestScore.bottom);
            _scoreUI.SetScore(0, 0);

            if (gameType != GameType.Remote)
            {
                _paddlesController.CreateLocalPaddles(gameType);
                _ball.OnStartServer();
                _remote = false;
                StartRound();
            }
            else
            {
                _paddlesController.InitializeRemoteControllers();
                _waiting = true;
                _remote = true;
            }
        }

        public void QuitGame()
        {
            _paddlesController.Reset();
            _ball.Deactivate();
            _ball.Disappear();
            _countInUI.Stop();
            _running = false;
        }

        public void StartRound()
        {
            Match.Instance.OnScoreChanged += HandleScoreChange;
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

            if (_waiting)
            {
                if (Match.Instance != null)
                {
                    _waiting = false;
                    StartRound();
                }
            }
        }

        private void Score(bool bottom)
        {
            var match = Match.Instance;
            
            if (!_remote || Mirror.NetworkServer.active)
            {
                if (bottom) match.ScoreBottom++;
                else match.ScoreTop++;
            }
        }

        private void HandleScoreChange(int bottom, int top)
        {
            _scoreUI.SetScore(top, bottom);
            if (bottom + top > _bestScore.bottom + _bestScore.top)
            {
                Data.DataManager.Instance.SetBestScore(bottom, top);
            }
        }
    }
}