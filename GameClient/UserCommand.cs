using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class UserCommand
    {
        private int clientId;
        private bool up;
        private bool down;
        private bool left;
        private bool right;
        private bool kick;
        private int kickForce;

        public UserCommand()
        {
            this.ClientId = 0;
            this.Up = false;
            this.Down = false;
            this.Left = false;
            this.Right = false;
            this.Kick = false;
            this.KickForce = 0;
        }

        public UserCommand(int clientId, bool up, bool down, bool left, bool right, bool kick, int kickForce)
        {
            this.clientId = clientId;
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
            this.kick = kick;
            this.kickForce = kickForce;
        }

        public bool Up { get => up; set => up = value; }
        public bool Down { get => down; set => down = value; }
        public bool Left { get => left; set => left = value; }
        public bool Right { get => right; set => right = value; }
        public bool Kick { get => kick; set => kick = value; }
        public int KickForce { get => kickForce; set => kickForce = value; }
        public int ClientId { get => clientId; set => clientId = value; }
    }
}
