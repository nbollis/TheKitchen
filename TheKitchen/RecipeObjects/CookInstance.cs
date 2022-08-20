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

        public CookInstance() { }
        
        public CookInstance(int rating, string notes, TimeSpan productionTime)
        {
            Rating = rating;
            Notes = notes;
            ProductionTime = productionTime;
            DateProduced = DateTime.Now;
        }

        public static bool TryParseCookInstanceFromTxt(string cookInstanceLine, out CookInstance cookInstance, out string error)
        {
            try
            {
                cookInstance = ParseCookInstanceFronString(cookInstanceLine);
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                cookInstance = null;
                error = $"Cook Instance failed to Parse due to {ex.Message}";
                return false;
            }
        }

        public static CookInstance ParseCookInstanceFronString(string cookInstanceLine)
        {
            CookInstance cookInstance = new();
            string[] list = cookInstanceLine.Split(':');
            cookInstance.DateProduced = DateTime.Parse(list[0]);
            if (list.Length == 2)
            {
                cookInstance.Notes = list[1].Trim();
            }
            if (list.Length == 3)
            {
                cookInstance.Rating = int.Parse(list[1]);
                cookInstance.Notes = list[2].Trim();
            }
            return cookInstance;
        }

        public override string ToString()
        {
            return DateProduced.Date.ToString() + " Ratiing: " + Rating + " " + Notes;
        }
    }
}
