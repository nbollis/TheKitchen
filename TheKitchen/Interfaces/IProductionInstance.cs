using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public interface IProductionInstance
    {
        public DateTime DateProduced { get;  }
        public string Date { get; }
        public TimeSpan ProductionTime { get; }
        public string Notes { get; }
        public int Rating { get; }
        
    }
}
