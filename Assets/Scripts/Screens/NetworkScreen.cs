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
            _networkController.OnDisconnected += HandleDisconnection;
        }

        public void Host()
        {
            OpenGame();
            _networkController.StartHost();
        }

        public void Connect()
        {
            OpenGame();
            _networkController.StartClient();
        }

        public override void Close()
        {
            _networkController.OnDisconnected -= HandleDisconnection;
            _networkController.StopClient();
            _networkController.StopServer();
            base.Close();
        }

        private void OpenGame()
        {
            gameObject.SetActive(false);
            _gameScreen.OpenRemote(onDone: Close);
        }

        private void HandleDisconnection()
        {
            _gameScreen.Close();
            Close();
        }
    }
}