using UnityEngine;
using Mirror;
using System;

namespace Pong.Game.Network
{
    public sealed class Match : NetworkBehaviour
    {
        public static Match Instance { get; private set; }
        public event Action<int, int> OnScoreChanged;

        [SyncVar(hook = nameof(BottomScoreChange))] public int ScoreBottom = 0;
        [SyncVar(hook = nameof(TopScoreChange))] public int ScoreTop = 0;

        private void BottomScoreChange (int oldScore, int newScore) 
        {
            ScoreBottom = newScore;
            OnScoreChanged?.Invoke(ScoreBottom, ScoreTop);
        }

        private void TopScoreChange (int oldScore, int newScore) 
        {
            ScoreTop = newScore;
            OnScoreChanged?.Invoke(ScoreBottom, ScoreTop);
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}