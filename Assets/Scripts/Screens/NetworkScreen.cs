using UnityEngine.UI;
using UnityEngine;
using Pong.Game;
using System;

namespace Pong.Screens
{
    public sealed class NetworkScreen : Screen
    {
        [SerializeField] private GameScreen _gameScreen = default;
        [SerializeField] private NetworkController _networkController = default;

        public override void Open(Action onDone)
        {
            base.Open(onDone);
            _networkController.OnConnected += HandleConnection;
            _networkController.OnDisconnected += HandleDisconnection;
        }

        public void Host()
        {
            _networkController.StartHost();
        }

        public void Connect()
        {
            _networkController.StartClient();
        }

        public override void Close()
        {
            base.Close();
            _networkController.OnConnected -= HandleConnection;
            _networkController.OnDisconnected -= HandleDisconnection;
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