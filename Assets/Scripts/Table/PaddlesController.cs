using Pong.Game.PaddleControllers;
using UnityEngine;

namespace Pong.Game
{
    public sealed class PaddlesController : MonoBehaviour
    {
        [SerializeField] private PaddleControllerFactory _paddleControllerFactory = default;
        [SerializeField] private Paddle _bottomPaddle = default;
        [SerializeField] private Paddle _topPaddle = default;

        public GameObject CreateRemotePaddle(bool bottom)
        {
            return bottom ? _bottomPaddle.gameObject : _topPaddle.gameObject;
        }

        public void InitializePaddles(GameType gameType)
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
            else if (gameType == GameType.Remote)
            {
                _bottomPaddle.SetController(_paddleControllerFactory.GetController(PaddleType.Remote, true));
                _topPaddle.SetController(_paddleControllerFactory.GetController(PaddleType.Remote, false));
            }
        }
    }
}