using UnityEngine;
using System;
using Mirror;

namespace Pong.Game
{
    public sealed class NetworkController : NetworkManager
    {
        public event Action OnConnected;
        public event Action OnDisconnected;

        [SerializeField] private GameObject _matchPrefab;

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
                var match = Instantiate(_matchPrefab);
                NetworkServer.Spawn(match.gameObject);
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
        }
    }
}
