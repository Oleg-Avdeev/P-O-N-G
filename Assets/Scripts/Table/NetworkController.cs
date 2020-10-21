using UnityEngine;
using System;
using Mirror;

namespace Pong.Game
{
    public sealed class NetworkController : NetworkManager
    {
        public event Action OnConnected;
        public event Action OnDisconnected;

        private Func<bool, GameObject> GetPaddle;

        public void SetPaddleCreationFunction(Func<bool, GameObject> getFunction)
        {
            GetPaddle = getFunction;
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            Debug.Log("Player connected!");

            GameObject player = GetPaddle(numPlayers == 0);
            NetworkServer.AddPlayerForConnection(conn, player);

            if (numPlayers == 2)
            {
                OnConnected?.Invoke();
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}
