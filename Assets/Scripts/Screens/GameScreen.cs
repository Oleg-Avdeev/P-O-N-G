using UnityEngine.UI;
using UnityEngine;
using Pong.Game;
using System;

namespace Pong.Screens
{
    public sealed class GameScreen : Screen
    {
        [SerializeField] private GameController _gameController = default;

        public override void Open(Action onDone)
        {
            _gameController.StartGame(GameType.Local);
            base.Open(onDone);
        }

        public void OpenPvE(Action onDone)
        {
            _gameController.StartGame(GameType.PvE);
            base.Open(onDone);
        }

        public void OpenRemote(Action onDone)
        {
            _gameController.StartGame(GameType.Remote);
            base.Open(onDone);
        }

        public override void Close()
        {
            _gameController.QuitGame();
            base.Close();
        }
    }
}