using UnityEngine;

namespace Pong.Game.PaddleControllers
{
    public sealed class PaddleControllerFactory : MonoBehaviour
    {
        public static PaddleControllerFactory Instance { get; private set; }

        [SerializeField] private Camera _gameCamera = default;
        [SerializeField] private Ball _ball = default;

        private void Awake()
        {
            Instance = this;
        }

        public IPaddleController GetController(PaddleType paddleType, bool isBottom)
        {
            switch (paddleType)
            {
                case PaddleType.AI : return new AIController(_ball);
                case PaddleType.Player : return new PlayerController(isBottom, _gameCamera);
            }
            return null;
        }
    }
}