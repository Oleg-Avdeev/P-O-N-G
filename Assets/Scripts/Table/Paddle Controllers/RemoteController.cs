using UnityEngine;

namespace Pong.Game.PaddleControllers
{
    public sealed class RemoteController : IPaddleController
    {
        public RemoteController()
        {
        }

        float IPaddleController.GetPosition()
        {
            return 0;
        }
    }
}