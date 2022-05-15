using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    // TODO implement a command so that when you click on the tag, it adds
    // that to the filter and right click takes it away, possibly could have
    // them appear to be pushed buttons, or turn them into buttons later
    public class TagViewModel : BaseViewModel
    {
        public string Tag { get; set; }
        public string RGB { get; set; }

    }
}
