using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public interface IItem
    {
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}
