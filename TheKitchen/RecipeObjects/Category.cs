using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    [Serializable]
    public class Category
    {
        public string Name { get; set; }    
        public string Description { get; set; } // not yet implemented, make it the mouseover effect
        public string RGB { get; set; } 
        
        public Category(string name, string rGB, string description = "")
        {
            Name = name;
            RGB = rGB;
            Description = description;
        }

        public Category() { }
    }
}
