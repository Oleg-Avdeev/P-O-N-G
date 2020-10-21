using UnityEngine.UI;
using UnityEngine;
using Pong.Game;
using System;

namespace Pong.Screens
{
    public sealed class NetworkScreen : Screen
    {
        [SerializeField] private GameScreen _gameScreen = default;
        // [SerializeField] private NetworkController _networkController = default;

        public override void Open(Action onDone)
        {
            base.Open(onDone);
        }

        public void Host()
        {
            OpenGame();
        }

        public void Connect()
        {
            OpenGame();
        }

        public override void Close()
        {
            base.Close();
        }

        private void OpenGame()
        {
            gameObject.SetActive(false);
            _gameScreen.OpenRemote(onDone: Close);
        }

        private void HandleConnection()
        {
            OpenGame();
        }

        private void HandleDisconnection()
        {
            _gameScreen.Close();
            Close();
        }
    }
}