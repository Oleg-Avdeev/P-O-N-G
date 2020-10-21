using Pong.Game.PaddleControllers;
using UnityEngine;

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
        }

        private void FixedUpdate()
        {
            _rigidBody.MovePosition(new Vector2(_nextPosition, _rigidBody.position.y));
        }

        private void Update()
        {
            _nextPosition = _controller.GetPosition();
        }
    }
}