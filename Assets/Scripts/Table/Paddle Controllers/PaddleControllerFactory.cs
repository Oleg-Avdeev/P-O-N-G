using UnityEngine;

namespace Pong.Game.PaddleControllers
{
    public sealed class PaddleControllerFactory : MonoBehaviour
    {
        [SerializeField] private Camera _gameCamera = default;

        public IPaddleController GetController(PaddleType paddleType, bool isBottom)
        {
            switch (paddleType)
            {
                case PaddleType.AI :     break;
                case PaddleType.Player : break;
                case PaddleType.Remote : break;
            }
            return null;
        }
    }
}