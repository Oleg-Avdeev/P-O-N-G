using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;

namespace Pong.Game.UI
{
    public sealed class CountIn : MonoBehaviour
    {
        [SerializeField] private Text _counterText = default;
        private Action _onComplete;

        public void Show(float seconds, Action onComplete)
        {
            _onComplete = onComplete;
            _counterText.gameObject.SetActive(true);

            DOVirtual.Float(seconds, 0, seconds, x => {
                _counterText.text = Mathf.Floor(x + 1).ToString();
                _counterText.transform.localScale = Vector3.one * (x % 1.0f);
            }).OnComplete(OnComplete).SetEase(Ease.Linear);
        }

        private void OnComplete()
        {
            _counterText.gameObject.SetActive(false);
            _counterText.text = "";
            _onComplete?.Invoke();
        }
    }
}