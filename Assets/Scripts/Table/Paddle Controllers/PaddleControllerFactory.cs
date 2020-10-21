using UnityEngine;

namespace Pong.Game.PaddleControllers
{
    public sealed class PaddleControllerFactory : MonoBehaviour
    {
        [SerializeField] private Camera _gameCamera = default;
        [SerializeField] private Ball _ball = default;

        public IPaddleController GetController(PaddleType paddleType, bool isBottom)
        {
            switch (paddleType)
            {
                case PaddleType.AI : return new AIController(_ball);
                case PaddleType.Player : return new PlayerController(isBottom, _gameCamera);
                case PaddleType.Remote : return new RemoteController();
            }
            return null;
        }
    }
}