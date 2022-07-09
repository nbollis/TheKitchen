using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class ListItemSelectedEventArgs : EventArgs
    {
        public string Name { get; set; }
        public ListItemSelectedEventArgs(string name)
        {
            Name = name.Trim();
        }
    }
}
