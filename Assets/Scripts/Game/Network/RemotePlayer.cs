using Pong.Game.PaddleControllers;
using UnityEngine;
using System;
using Mirror;

namespace Pong.Game.Network
{
    public sealed class RemotePlayer : NetworkBehaviour, IPaddleController
    {
        public bool IsBottom => _bottom;
        
        [SyncVar(hook = nameof(SetOrientation))] 
        private bool _bottom;

        private IPaddleController _controller;

        public override void OnStartServer()
        {
            base.OnStartServer();
        }

        public override void OnStartClient()
        {
            NetworkController.Instance.RegisterPlayer(this);
        }

        public void SetOrientation(bool oldValue, bool bottom)
        {
            _bottom = bottom;
        }

        public void SetController(IPaddleController controller)
        {
            _controller = controller;
        }

        bool IPaddleController.CanPlay()
        {
            return true;
        }

        float IPaddleController.GetPosition()
        {
            if (isLocalPlayer)
            {
                var x = _controller.GetPosition();
                var y = _bottom ? -10 : 10;
                transform.position = new Vector3(x, y, 0);
            }
            return transform.position.x;
        }
    }
}