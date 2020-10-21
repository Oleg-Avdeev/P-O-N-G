using UnityEngine;

namespace Pong.Data
{
    public sealed class DataManager : IDataManager
    {
        private const string _hsvKey = "hsv{0}";
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
            var hsb = _instance.GetBallHSB();
            return ColorUtility.HSVtoRGB(hsb.x, hsb.y, hsb.z);
        }

        Vector3 IDataManager.GetBallHSB()
        {
            var h = PlayerPrefs.GetFloat(string.Format(_hsvKey, 0), 1f);
            var s = PlayerPrefs.GetFloat(string.Format(_hsvKey, 1), 1f);
            var b = PlayerPrefs.GetFloat(string.Format(_hsvKey, 2), 1f);
            return new Vector3(h, s, b);
        }

        void IDataManager.SetBallHSB(float h, float s, float b)
        {
            PlayerPrefs.SetFloat(string.Format(_hsvKey, 0), h);
            PlayerPrefs.SetFloat(string.Format(_hsvKey, 1), s);
            PlayerPrefs.SetFloat(string.Format(_hsvKey, 2), b);
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