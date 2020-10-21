using UnityEngine;
using System;

namespace Pong.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        protected Action _onDone;

        public virtual void Open(Action onDone) 
        {
            _onDone = onDone;
            gameObject.SetActive(true);
        }

        public virtual void Close() 
        {
            _onDone?.Invoke();
            gameObject.SetActive(false);
        }

    }
}