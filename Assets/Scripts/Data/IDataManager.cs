using UnityEngine;

namespace Pong.Data
{
    public interface IDataManager
    {
        Color GetBallColor();
        Vector3 GetBallHSB();
        void SetBallHSB(float h, float s, float b);

        (int score1, int score2) GetBestScore();
        void SetBestScore(int score1, int score2);
    }
}