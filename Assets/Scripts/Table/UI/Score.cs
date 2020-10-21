using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;

namespace Pong.Game.UI
{
    public sealed class Score : MonoBehaviour
    {
        private const string _bestScoreTemplate = "Best {0}:{1}";

        [SerializeField] private Text _topScoreText = default;
        [SerializeField] private Text _bottomScoreText = default;
        [SerializeField] private Text _bestScoreText = default;
        
        public void SetScore(int topScore, int bottomScore)
        {
            _topScoreText.text = topScore.ToString();
            _bottomScoreText.text = bottomScore.ToString();
        }

        public void SetBestScore(int topScore, int bottomScore)
        {
            if (topScore == 0 && bottomScore == 0)
            {
                _bestScoreText.text = "";
            }
            else
            {
                _bestScoreText.text = string.Format(_bestScoreTemplate, topScore, bottomScore);
            }
        }

    }
}