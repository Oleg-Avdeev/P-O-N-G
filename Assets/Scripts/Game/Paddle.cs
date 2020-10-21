using Pong.Game.PaddleControllers;
using UnityEngine;
using Mirror;

namespace Pong.Game
{
    public sealed class Paddle : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody = default;

        private IPaddleController _controller;
        private float _nextPosition = 0;

        public void SetController(IPaddleController controller)
        {
            _controller = controller;
            gameObject.SetActive(_controller != null);
        }

        private void FixedUpdate()
        {
            if (_controller != null && _controller.CanPlay())
            {
                _rigidBody.MovePosition(new Vector2(_nextPosition, _rigidBody.position.y));
            }
        }

        private void Update()
        {
            if (_controller != null && _controller.CanPlay())
            {
                _nextPosition = _controller.GetPosition();
            }
        }
    }
}