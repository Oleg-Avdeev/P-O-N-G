using UnityEngine;

namespace Pong.Data
{
    public sealed class DataManager : IDataManager
    {
        private const string _colorKey = "color{0}";
        private const string _scoreKey = "score{0}";

        private static IDataManager _instance;
        public static IDataManager Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new DataManager();
                return _instance;
            }
        }

        Color IDataManager.GetBallColor()
        {
            var r = PlayerPrefs.GetFloat(string.Format(_colorKey, 0), 1f);
            var g = PlayerPrefs.GetFloat(string.Format(_colorKey, 1), 1f);
            var b = PlayerPrefs.GetFloat(string.Format(_colorKey, 2), 1f);
            return new Color(r,g,b,1);
        }

        void IDataManager.SetBallColor(Color color)
        {
            PlayerPrefs.SetFloat(string.Format(_colorKey, 0), color.r);
            PlayerPrefs.SetFloat(string.Format(_colorKey, 1), color.g);
            PlayerPrefs.SetFloat(string.Format(_colorKey, 2), color.b);
        }

        (int score1, int score2) IDataManager.GetBestScore()
        {
            var s1 = PlayerPrefs.GetInt(string.Format(_scoreKey, 0), 0);
            var s2 = PlayerPrefs.GetInt(string.Format(_scoreKey, 1), 0);
            return (s1, s2);
        }

        void IDataManager.SetBestScore(int score1, int score2)
        {
            PlayerPrefs.SetInt(string.Format(_scoreKey, 0), score1);
            PlayerPrefs.SetInt(string.Format(_scoreKey, 1), score2);
        }
    }
}