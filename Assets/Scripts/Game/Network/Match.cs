using UnityEngine;
using Mirror;

namespace Pong.Game.Network
{
    public sealed class Match : NetworkBehaviour
    {
        public static Match Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}