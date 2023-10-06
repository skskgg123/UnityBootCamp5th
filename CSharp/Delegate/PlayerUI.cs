using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    internal class PlayerUI
    {
        private string _hpValue;

        public void Refresh(int hpValue)
        {
            _hpValue = hpValue.ToString();
        }
    }
}
