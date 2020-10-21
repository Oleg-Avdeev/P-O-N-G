using Pong.Game.PaddleControllers;
using UnityEngine;

namespace Pong.Game
{
    public sealed class PaddlesController : MonoBehaviour
    {
        [SerializeField] private PaddleControllerFactory _paddleControllerFactory = default;
        [SerializeField] private Paddle _paddlePrefab = default;
        
        [SerializeField] private Transform _top = default;
        [SerializeField] private Transform _bottom = default;

        private Paddle _bottomPaddle;
        private Paddle _topPaddle;

        public void Reset()
        {
            if (_bottomPaddle != null)
            {
                Destroy(_bottomPaddle.gameObject);
                Destroy(_topPaddle.gameObject);
            }
        }

        public GameObject CreateRemotePaddle(bool bottom)
        {
            var point = bottom ? _bottom : _top;
            var p = Instantiate(_paddlePrefab, point.position, Quaternion.identity, _bottom.parent);
            p.SetController(_paddleControllerFactory.GetController(PaddleType.Remote, bottom));
            return p.gameObject;
        }

        public void CreateLocalPaddles(GameType gameType)
        {
            if (gameType == GameType.Local)
            {
                var bcontroller = _paddleControllerFactory.GetController(PaddleType.Player, true);
                var tcontroller = _paddleControllerFactory.GetController(PaddleType.Player, false);
                _bottomPaddle = CreatePaddle(bcontroller, true);
                _topPaddle = CreatePaddle(tcontroller, false);
            }
            else if (gameType == GameType.PvE)
            {
                var bcontroller = _paddleControllerFactory.GetController(PaddleType.Player, true);
                var tcontroller = _paddleControllerFactory.GetController(PaddleType.AI, false);
                _bottomPaddle = CreatePaddle(bcontroller, true);
                _topPaddle = CreatePaddle(tcontroller, false);
            }
        }

        private Paddle CreatePaddle(IPaddleController controller, bool bottom)
        {
            var point = bottom ? _bottom : _top;
            var paddle = Instantiate(_paddlePrefab, point.position, Quaternion.identity, _bottom.parent);
            paddle.SetController(controller);
            return paddle;
        }
    }
}