using Pong.Game.PaddleControllers;
using Pong.Game.Network;
using UnityEngine;

namespace Pong.Game
{
    public sealed class PaddlesController : MonoBehaviour
    {
        [SerializeField] private PaddleControllerFactory _paddleControllerFactory = default;
        [SerializeField] private Paddle _bottomPaddle = default;
        [SerializeField] private Paddle _topPaddle = default;

        public void InitializeRemoteControllers()
        {
            NetworkController.Instance.OnPlayerConnected += HandleRemotePlayer;
        }

        public void Reset()
        {
            NetworkController.Instance.OnPlayerConnected -= HandleRemotePlayer;
            _bottomPaddle.SetController(null);
            _topPaddle.SetController(null);
        }

        public void CreateLocalPaddles(GameType gameType)
        {
            if (gameType == GameType.Local)
            {
                _bottomPaddle.SetController(_paddleControllerFactory.GetController(PaddleType.Player, true));
                _topPaddle.SetController(_paddleControllerFactory.GetController(PaddleType.Player, false));
            }
            else if (gameType == GameType.PvE)
            {
                _bottomPaddle.SetController(_paddleControllerFactory.GetController(PaddleType.Player, true));
                _topPaddle.SetController(_paddleControllerFactory.GetController(PaddleType.AI, false));
            }
        }

        private void HandleRemotePlayer(RemotePlayer player)
        {
            player.SetController(_paddleControllerFactory.GetController(PaddleType.Player, player.IsBottom));
            Debug.LogWarning($"Remote Player {player.IsBottom} added! Controller {(IPaddleController)player}");

            if (player.IsBottom) _bottomPaddle.SetController(player);
            else _topPaddle.SetController(player);
        }

    }
}