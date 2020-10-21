using UnityEngine;

namespace Pong.Game.PaddleControllers
{
    public class PlayerController : IPaddleController
    {
        private bool _isBottomPlayer;
        private Camera _projectionCamera;
        private float _lastValue = 0;

        public PlayerController(bool isBottomPlayer, Camera camera)
        {
            _isBottomPlayer = isBottomPlayer;
            _projectionCamera = camera;
        }

        float IPaddleController.GetPosition()
        {
            Vector2 average = Vector2.zero;
            int count = 0;

            #if UNITY_ANDROID || UNITY_IOS
                var touches = Input.touches;
                foreach (var touch in touches)
                {
                    if (touch.position.y > Screen.height/2 ^ _isBottomPlayer)
                    {
                        average += touch.position;
                        count++;
                    }
                }

                if (count == 0)
                {
                    return _lastValue;
                }
            #else
                if (Input.mousePosition.y > Screen.height/2 ^ _isBottomPlayer)
                {
                    average = Input.mousePosition;
                    count = 1;
                }
                else
                {
                    return _lastValue;
                }
            #endif
            
            _lastValue = _projectionCamera.ScreenToWorldPoint(average / count).x;
            return _lastValue;
        }
    }
}