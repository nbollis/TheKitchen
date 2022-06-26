using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class CookInstance : IProductionInstance
    {
        public DateTime DateProduced { get; set; }
        public TimeSpan ProductionTime { get; set; }

        public string Notes { get; set; }
        public int Rating { get; set; }
        public string Date
        {
            get { return DateProduced.ToShortDateString(); }
            set { DateProduced = DateTime.Parse(value); }
        }

        /// <summary>
        /// An interface between the TimeSpan object and my prefered string representation
        /// </summary>
        public string Time
        {
            get { return Converters.GetTimeString(ProductionTime); }
            set { ProductionTime = Converters.GetTimeSpan(value); }
        }


        public CookInstance(string notes)
        {
            string[] list = notes.Split(':');
            DateProduced = DateTime.Parse(list[0]);
            Notes = list[1].Trim();
        }

        public CookInstance() { }
        
        public CookInstance(int rating, string notes, TimeSpan productionTime)
        {
            Rating = rating;
            Notes = notes;
            ProductionTime = productionTime;
            DateProduced = DateTime.Now;
        }

        public override string ToString()
        {
            return DateProduced.Date.ToString() + " Ratiing: " + Rating + " " + Notes;
        }
    }
}
