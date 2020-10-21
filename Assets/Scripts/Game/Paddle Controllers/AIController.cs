using UnityEngine;

namespace Pong.Game.PaddleControllers
{
    public sealed class AIController : IPaddleController
    {
        private Ball _ball;

        public AIController(Ball ball)
        {
            _ball = ball;
        }

        bool IPaddleController.CanPlay() => true;

        float IPaddleController.GetPosition()
        {
            return _ball.GetPosition.x;
        }
    }
}