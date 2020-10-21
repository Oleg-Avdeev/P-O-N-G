using UnityEngine;
using System;
using Mirror;

namespace Pong.Game.Network
{
    public sealed class NetworkController : NetworkManager
    {
        public static NetworkController Instance { get; private set; }

        public event Action<RemotePlayer> OnPlayerConnected;
        public event Action OnDisconnected;
        
        [SerializeField] private GameObject _matchPrefab = default;
        [SerializeField] private RemotePlayer _playerPrefab = default;

        public void Initialize() => Instance = this;

        public void RegisterPlayer(RemotePlayer player)
        {
            OnPlayerConnected?.Invoke(player);
        }

        public void Reset()
        {
            NetworkController.Instance.StopClient();
            NetworkController.Instance.StopServer();
        }

        public void SpawnPaddle(GameObject paddle)
        {
            NetworkServer.Spawn(paddle);
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            var player = Instantiate(_playerPrefab);
            player.SetOrientation(false, numPlayers == 0);
            NetworkServer.AddPlayerForConnection(conn, player.gameObject);

            if (numPlayers == 2)
            {
                var match = Instantiate(_matchPrefab);
                NetworkServer.Spawn(match.gameObject);
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            OnDisconnected?.Invoke();
            base.OnServerDisconnect(conn);
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            OnDisconnected?.Invoke();
            base.OnClientDisconnect(conn);
        }
    }
}
