using UnityEngine.UI;
using UnityEngine;
using Pong.Game;
using System;

namespace Pong.Screens
{
    public sealed class GameScreen : Screen
    {
        [SerializeField] private GameController _gameController = default;
        [SerializeField] private Material _ballMaterial = default;

        public override void Open(Action onDone)
        {
            var color = Data.DataManager.Instance.GetBallColor();
            _ballMaterial.SetColor("_Color", color);
            
            _gameController.StartGame();
            base.Open(onDone);
        }

        public override void Close()
        {
            _gameController.QuitGame();
            base.Close();
        }
    }
}