using Random = UnityEngine.Random;
using UnityEngine;
using DG.Tweening;
using System;
using Mirror;

namespace Pong.Game
{
    public sealed class Ball : NetworkBehaviour
    {
        public struct Parameters { public float Size; public float Speed; public float SpeedUpFactor; }

        public Vector2 GetPosition => _rigidBody.position;

        [SerializeField] private Rigidbody2D _rigidBody = default;
        [SerializeField] private Collider2D _collider = default;
        [SerializeField] private Transform _transform = default;
        [SerializeField] private float _appearDuration = 0.5f;
        [SerializeField] private float _maxSpeed = 8f;

        private float _speedUpFactor = 1f;
        private bool _simulating = false;
        private Vector2 _velocity;
        private Tween _animationTween;

        public override void OnStartServer()
        {
            base.OnStartServer();
            _simulating = true;
        }

        public void Appear(Parameters parameters)
        {
            Debug.Log($"Ball Parameters: {parameters.Size:0.00} {parameters.Speed:0.00} {parameters.SpeedUpFactor:0.00}");

            gameObject.SetActive(true);
            _transform.localScale = Vector3.zero;
            _transform.localPosition = Vector3.zero;
            _animationTween = _transform.DOScale(parameters.Size, _appearDuration).SetEase(Ease.InOutExpo);
            
            //Make sure that the ball always has Vy component
            float y = Random.Range(0f,1f) > 0.5f ? 1f : -1f;
            float x = Random.Range(-1f,1f);

            _velocity = new Vector2(x, y).normalized * parameters.Speed;
            
            _speedUpFactor = parameters.SpeedUpFactor;
        }

        public void Activate()
        {
            if (_simulating)
            {
                _rigidBody.isKinematic = false;
                _rigidBody.simulated = true;
                _collider.enabled = true;
                _rigidBody.velocity = _velocity;
            }
        }

        public void Deactivate()
        {
            if (_simulating)
            {
                _rigidBody.isKinematic = true;
                _rigidBody.simulated = false;
                _collider.enabled = false;
            }

            _animationTween.Kill(false);
        }

        [Server]
        public void Disappear(Action onComplete = null)
        {
            _animationTween = _transform.DOScale(0, _appearDuration).SetEase(Ease.InOutExpo).OnComplete(() => {
                onComplete?.Invoke();
            });
        }

        private void FixedUpdate()
        {
            if (_rigidBody.simulated)
            {
                _velocity = _rigidBody.velocity;
            }
        }

        // [ServerCallback]
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.contactCount > 0)
            {
                if (col.collider.name == "Borders")
                {
                    var normal = col.contacts[0].normal;
                    _rigidBody.velocity = Vector2.Reflect(_velocity, normal);
                }
                else
                {
                    var m = _velocity.magnitude;
                    var x = (_rigidBody.position.x - col.rigidbody.position.x);
                    var y = col.contacts[0].normal.y;
                    
                    if (Mathf.Abs(y) < 1) y = Mathf.Sign(y) * 1;
                    if (y == 0) y = Random.Range(-2, 2);

                    var direction = new Vector2(x, y);
                    _rigidBody.velocity = m * direction.normalized;

                    if (_rigidBody.velocity.magnitude < _maxSpeed)
                    {
                        _rigidBody.velocity = _rigidBody.velocity * _speedUpFactor;
                    }
                }
            }
        }
    }
}