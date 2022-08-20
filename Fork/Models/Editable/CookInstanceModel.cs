using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKitchen;

namespace Fork
{
    public class CookInstanceModel : BaseViewModel, IProductionInstance
    {
        public static CookInstanceModel Instance => new CookInstanceModel();

        public DateTime DateProduced { get; set; }
        public TimeSpan ProductionTime { get; set; }

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
            get
            {
                string[] components = ProductionTime.ToString().Split(':');
                string time = "";
                if (Double.Parse(components[0]) > 9)
                {
                    time = components[0] + "h ";
                }
                else if (Double.Parse(components[0]) > 0)
                {
                    time = components[0].Replace("0", "") + "h ";
                }
                time += components[1] + "m";
                return time;
            }
            set
            {
                int hours = 0;
                int minutes;
                if (int.TryParse(value.Split('h')[0], out int tempHours))
                {
                    hours = tempHours;
                    minutes = int.Parse(value.Split('h')[1].Replace("m", ""));
                }
                else
                {
                    minutes = int.Parse(value.Split("m")[0]);
                }
                ProductionTime = new TimeSpan(hours, minutes, 0);
            }
        }
        public string Notes { get; set; }
        public int Rating { get; set; }

        public CookInstanceModel()
        {
            DateProduced = DateTime.Now;
            Notes = "Here are some sample notes, the quick brown fox jumped over the lazy cat, go hand a salami I'm a lasagna hog";
            Rating = 8;
            ProductionTime = TimeSpan.FromMinutes(45);
        }
    }
}
