using UnityEngine;

namespace Pong.Game
{
    public sealed class Border : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody = default;
        [SerializeField] private Camera _gameCamera = default;
        [SerializeField] private float _offset = 1;
        
        private void Start()
        {
            float worldScreenHeight = _gameCamera.orthographicSize * 2;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            float x = Mathf.Sign(_rigidBody.position.x) * (worldScreenWidth/2 + _offset);
            _rigidBody.position = new Vector2(x, _rigidBody.position.y);
        }
    }
}