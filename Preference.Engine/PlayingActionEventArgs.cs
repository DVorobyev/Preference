using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Preference.Engine
{
    public class PlayingActionEventArgs : EventArgs
    {
        public Hand Hand { get; set; }
        public Card Card { get; set; }
    }
}
