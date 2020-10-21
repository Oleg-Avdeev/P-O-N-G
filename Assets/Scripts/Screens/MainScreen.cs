using UnityEngine.UI;
using UnityEngine;

namespace Pong.Screens
{
    public sealed class MainScreen : MonoBehaviour
    {
        [SerializeField] private Screen _settingsSceen = default;
        [SerializeField] private Screen _gameScreen = default;
        [SerializeField] private Screen _networkScreen = default;

        public void OpenSettings()
        {
            Hide();
            _settingsSceen.Open(onDone: Open);
        }

        public void OpenGameScreen()
        {
            Hide();
            _gameScreen.Open(onDone: Open);
        }

        public void OpenNetworkScreen()
        {
            Hide();
            _networkScreen.Open(onDone: Open);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}