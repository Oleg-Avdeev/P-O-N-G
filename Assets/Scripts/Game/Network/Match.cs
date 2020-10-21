using UnityEngine;
using Mirror;
using System;

namespace Pong.Game.Network
{
    public sealed class Match : NetworkBehaviour
    {
        public static Match Instance { get; private set; }
        public event Action<int, int> OnScoreChanged;

        [SyncVar(hook = nameof(BottomScoreChange))] private int _scoreBottom = 0;
        [SyncVar(hook = nameof(TopScoreChange))] private int _scoreTop = 0;
        private bool _local = false;

        public void SetAsLocal()
        {
            _local = true;
        }

        public void Score(bool bottom)
        {
            if (_local || Mirror.NetworkServer.active)
            {
                if (bottom) _scoreBottom++;
                else _scoreTop++;
                
                OnScoreChanged?.Invoke(_scoreBottom, _scoreTop);
            }
        }

        private void BottomScoreChange (int oldScore, int newScore) 
        {
            _scoreBottom = newScore;
            OnScoreChanged?.Invoke(_scoreBottom, _scoreTop);
        }

        private void TopScoreChange (int oldScore, int newScore) 
        {
            _scoreTop = newScore;
            OnScoreChanged?.Invoke(_scoreBottom, _scoreTop);
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}