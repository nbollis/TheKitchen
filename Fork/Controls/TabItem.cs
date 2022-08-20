using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class TabItem
    {
        public string Header { get; set; }
        public BaseViewModel Content { get; set; }

        public TabItem(string header, BaseViewModel content)
        {
            Header = header;
            Content = content;
        }

        public override string ToString()
        {
            return Header;
        }
    }
}
