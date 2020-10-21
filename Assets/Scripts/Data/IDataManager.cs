using UnityEngine;

namespace Pong.Data
{
    public interface IDataManager
    {
        Color GetBallColor();
        void SetBallColor(Color color);

        (int score1, int score2) GetBestScore();
        void SetBestScore(int score1, int score2);
    }
}