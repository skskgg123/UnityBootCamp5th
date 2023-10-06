using System;

namespace Delegates
{
    internal class Player
    {
        public float hp
        {
            get
            {
                return _hp;
            }
            set 
            {
                _hp = value;
            }
        }

        private float _hp;
    }
}
