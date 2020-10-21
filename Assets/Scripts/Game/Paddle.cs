using Pong.Game.PaddleControllers;
using UnityEngine;
using Mirror;

namespace Pong.Game
{
    public sealed class Paddle : NetworkBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody = default;

        private IPaddleController _controller;
        private float _nextPosition = 0;
        private bool local;

        private void Start()
        {
            if (_controller == null)
            {
                bool bottom = _rigidBody.position.y < 0;
                _controller = PaddleControllerFactory.Instance.GetController(PaddleType.Remote, bottom);
            }
        }

        public void SetController(IPaddleController controller)
        {
            _controller = controller;
            
            if (_controller != null)
            {
                gameObject.SetActive(true);
            }
        }

        private void FixedUpdate()
        {
            if (_controller != null && _controller.CanPlay(isLocalPlayer))
            {
                _rigidBody.MovePosition(new Vector2(_nextPosition, _rigidBody.position.y));
            }
        }

        private void Update()
        {
            if (_controller != null && _controller.CanPlay(isLocalPlayer))
            {
                _nextPosition = _controller.GetPosition();
            }
        }
    }
}