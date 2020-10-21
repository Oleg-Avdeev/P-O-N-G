using UnityEngine;
using System;
using Mirror;

namespace Pong.Game
{
    [AddComponentMenu("")]
    public sealed class NetworkController : NetworkManager
    {
        public event Action OnConnected;
        public event Action OnDisconnected;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            Debug.Log("Player connected!");
            
            if (numPlayers == 2)
            {
                OnConnected?.Invoke();
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            OnDisconnected?.Invoke();
            base.OnServerDisconnect(conn);
        }
    }
}
