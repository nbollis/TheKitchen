using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class CookInstance
    {
        public DateTime dateCooked;
        public string Notes;
        public double rating;
        public double CookTime { get; set; }

        public CookInstance(string notes)
        {
            string[] list = notes.Split(':');
            dateCooked = DateTime.Parse(list[0]);
            Notes = list[1].Trim();
        }
        
        public CookInstance()
        {

        }

        public override string ToString()
        {
            return dateCooked.Date.ToString() + " Ratiing: " + rating + " " + Notes;
        }
    }
}
